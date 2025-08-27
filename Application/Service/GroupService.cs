using Domain.DTO.Expense;
using Domain.DTO.Group;
using Domain.Interface.Context;
using Domain.Interface.Database;
using Domain.Interface.Mapper.ExpenseMapper;
using Domain.Interface.Mapper.GroupMapper;
using Domain.Interface.Mapper.UserMapper;
using Domain.Interface.Repository;
using Domain.Interface.Service;
using Microsoft.EntityFrameworkCore;

namespace Application.Service
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _gr;
        private readonly IUserRepository _ur;
        private readonly ICurrentUserService _acessor;
        private readonly IGroupMP _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IUserMP _userMP;
        private readonly IExpenseMP _expenseMP;

        public GroupService(IExpenseMP expenseMP, IUserMP userMP, IUnitOfWork uow, IGroupMP mapper, IGroupRepository gr, IUserRepository ur, ICurrentUserService acessor)
        {
            _expenseMP = expenseMP;
            _userMP = userMP;
            _uow = uow;
            _mapper = mapper;
            _gr = gr;
            _ur = ur;
            _acessor = acessor;

        }

        public async Task<GroupResponseDTO> CreateGroup(GroupCreateDTO dto)
        {
            var currentUser = _acessor.UserId;

            if (currentUser == null) throw new NullReferenceException(nameof(currentUser));

            var gpzada = _mapper.ToEntity(dto);

            var user = await _ur
                                .Find()
                                .FirstOrDefaultAsync(i => i.Id == currentUser.Value)
                                ?? throw new NullReferenceException();

            gpzada.Users.Add(user);
            gpzada.LeaderId = user.Id;


            await _gr.Create(gpzada);

            await _uow.CommitAsync();

            return _mapper.ToDTO(gpzada);

        }
        public async Task<string> GetGroupName(int id)
        {
            var gp = await _gr
                            .Find()
                            .FirstOrDefaultAsync(i => i.Id == id)
                            ?? throw new NullReferenceException("Grupo não encontrado");

            return gp.Name;
        }

        public async Task<GroupSummaryDTO> AddMember(int gpId, string userEmail)
        {

            var userId = _acessor.UserId;

            var user = await _ur
                                .Find()
                                .FirstOrDefaultAsync(i => i.Id == userId)
                                ?? throw new NullReferenceException();

            var group = await _gr
                                 .Find()
                                 .Include(f => f.Users)
                                 .Include(f => f.Expenses)
                                 .FirstOrDefaultAsync(i => i.Id == gpId)
                                 ?? throw new NullReferenceException();

            if (!group.Users.Any(i => i.Id == user.Id)) throw new ArgumentException("Usuário sem permissão");

            var userToAdd = await _ur
                                    .Find()
                                    .FirstOrDefaultAsync(i => i.Email == userEmail)
                                    ?? throw new ArgumentException("Usuário não encontrado");


            group.Users.Add(userToAdd);

            await _uow.CommitAsync();


            var exp = new List<ExpenseDetailDTO>();

            foreach (var expense in group.Expenses)
            {
                var dto = new ExpenseDetailDTO(expense.Description, expense.Value, expense.Date, _userMP.ToSummary(expense.Payer), expense.Participants.Select(_userMP.ToSummary).ToList(), _mapper.ToDTO(group));
                exp.Add(dto);
            }

            return new GroupSummaryDTO(group.Name, group.LeaderId, group.Users.Select(_userMP.ToSummary).ToList(), group.IsPublic, exp);

        }

        public async Task<GroupSummaryDTO> RemoveMember(int groupId, int userToRemoveId)
        {
            var userInContext = _acessor.UserId;

            var user = await _ur
                                .Find()
                                .FirstOrDefaultAsync(i => i.Id == userInContext)
                                ?? throw new NullReferenceException();

            var userToRemove = await _ur
                                        .Find()
                                        .FirstOrDefaultAsync(i => i.Id == userToRemoveId)
                                        ?? throw new NullReferenceException();

            var gp = await _gr
                            .Find()
                            .Include(f => f.Users)
                            .Include(f => f.Expenses)
                            .FirstOrDefaultAsync(i => i.Id == groupId)
                            ?? throw new NullReferenceException();

            if (user.Id != gp.LeaderId) throw new ArgumentException("Usuário sem permissão");

            gp.Users.Remove(userToRemove);

            await _uow.CommitAsync();
            var exp = new List<ExpenseDetailDTO>();

            foreach (var expense in gp.Expenses)
            {
                var dto = new ExpenseDetailDTO(expense.Description, expense.Value, expense.Date, _userMP.ToSummary(expense.Payer), expense.Participants.Select(_userMP.ToSummary).ToList(), _mapper.ToDTO(gp));
                exp.Add(dto);
            }


            return new GroupSummaryDTO(gp.Name, gp.LeaderId, gp.Users.Select(_userMP.ToSummary).ToList(), gp.IsPublic,exp);

        }

        public async Task<bool> DeleteGroup(int groupId)
        {
            var userInContext = _acessor.UserId;

            var group = await _gr
                                .Find()
                                .FirstOrDefaultAsync(f => f.Id == groupId)
                                ?? throw new ArgumentNullException("Grupo não encontrado");

            if (group.LeaderId != userInContext) throw new ArgumentException("Sem acesso para excluir grupo");


            if (group.Users.Any())
            {
                return false;
                throw new ArgumentException("Não é possível remover grupos contendo participantes.");
            }
            _gr.Delete(group);

            await _uow.CommitAsync();
            return true;
        }

        public async Task<GroupSummaryDTO> RenameGroup(string newName, int groupId)
        {
            var userInContext = _acessor.UserId;

            var group = await _gr
                                .Find()
                                .Include(i => i.Users)
                                .Include(i => i.Expenses)
                                .FirstOrDefaultAsync(f => f.Id == groupId)
                                ?? throw new NullReferenceException();

            if (group.LeaderId != userInContext) throw new ArgumentException("Usuário sem permissão");

            group.Name = newName;

            await _uow.CommitAsync();
            var novo = new List<ExpenseDetailDTO>();

            foreach(var expense in group.Expenses)
            {
                var dto = new ExpenseDetailDTO(expense.Description, expense.Value, expense.Date, _userMP.ToSummary(expense.Payer), expense.Participants.Select(_userMP.ToSummary).ToList(),_mapper.ToDTO(group));
                novo.Add(dto);
            }

            return new GroupSummaryDTO(group.Name, group.LeaderId, group.Users.Select(_userMP.ToSummary).ToList(), group.IsPublic, novo);


        }

    }
}
