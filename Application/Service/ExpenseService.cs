

using Application.DTO.Expense;
using Application.Interface.Mapper.ExpenseMapper;
using Application.Interface.Mapper.GroupMapper;
using Application.Interface.Mapper.UserMapper;
using Application.Interface.Service;
using Domain.Interface.Context;
using Domain.Interface.Database;
using Domain.Interface.Repository;
using Microsoft.EntityFrameworkCore;

namespace Application.Service
{
    public class ExpenseService:IExpenseService
    {
        private readonly IExpenseRepository _context;
        private readonly IExpenseMP _mapper;
        private readonly IUnitOfWork _work;
        private readonly ICurrentUserService _acessor;
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserMP _userMP;
        private readonly IGroupMP _groupMP;

        public ExpenseService(IGroupMP groupMP, IUserMP userMP, IUserRepository userRepository, IGroupRepository groupRepository, ICurrentUserService acessor, IUnitOfWork work, IExpenseMP mapper, IExpenseRepository context)
        {
            _groupMP = groupMP;
            _userMP = userMP;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _work = work;
            _acessor = acessor;
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExpenseResponseDTO> GetById(int id)
        {
            var response = await _context
                                        .Find()
                                        .FirstOrDefaultAsync(x => x.Id == id)
                                        ?? throw new NullReferenceException("Não foi possível encontrar uma transação com este ID");

            return _mapper.ToResponse(response);

        }

        public async Task<ExpenseDetailDTO> GetExpenseIdWithGroup(int id)
        {
            var response = await _context
                                        .Find()
                                        .Include(i => i.Group)
                                        .Include(i => i.Payer)
                                        .Include(i => i.Participants)
                                        .FirstOrDefaultAsync(j => j.Id == id)
                                        ?? throw new NullReferenceException("Não foi possível encontrar essa transação no grupo");

            return new ExpenseDetailDTO(response.Id,response.Description, response.Value, response.Date, _userMP.ToSummary(response.Payer), response.Participants.Select(_userMP.ToSummary).ToList(), _groupMP.ToDTO(response.Group));
        }

        public async Task<ExpenseDetailDTO> CreateExpense(int groupId, ExpenseCreateDTO dto)
        {
            var user = await _userRepository
                                        .Find()
                                        .FirstOrDefaultAsync(i => i.Id == _acessor.UserId)
                                        ?? throw new NullReferenceException(); ;

            var group = await _groupRepository
                                        .Find()
                                        .Include(i => i.Users)
                                        .FirstOrDefaultAsync(j => j.Id == groupId)
                                        ?? throw new NullReferenceException();

            if (!group.Users.Any(i => i.Id == user.Id)) throw new ArgumentException("Usuário sem permissão.");

            if (!group.Users.Any(u => u.Id == dto.PaidByUserId)) throw new Exception("O usuário pagador informado não é membro deste grupo.");

            var newExpense = _mapper.ToEntity(dto);

            newExpense.GroupId = group.Id;

            var payer = await _userRepository
                                        .Find()
                                        .FirstOrDefaultAsync(i => i.Id == dto.PaidByUserId)
                                        ?? throw new NullReferenceException();

            var participants = await _userRepository
                                                .Find()
                                                .Where(i => dto.ParticipantsIds.Contains(i.Id))
                                                .ToListAsync();

            newExpense.Participants = participants;

            await _work.Expenses.Add(newExpense);

            await _work.CommitAsync();

            return new ExpenseDetailDTO(newExpense.Id, newExpense.Description, newExpense.Value, newExpense.Date, _userMP.ToSummary(newExpense.Payer), newExpense.Participants.Select(_userMP.ToSummary).ToList(), _groupMP.ToDTO(newExpense.Group));
        }

        public async Task<GroupBalanceDTO> GetGroupBalanceAsync(int groupId)
        {

            var gp = await _groupRepository
                                 .Find()
                                 .Include(i => i.Id == groupId)
                                 .Include(i => i.Users)
                                 .Include(i => i.Expenses)
                                    .ThenInclude(i => i.Payer)
                                 .Include(i => i.Expenses)
                                    .ThenInclude(i => i.Participants)
                                 .FirstOrDefaultAsync()
                                 ?? throw new NullReferenceException();

            var user = await _userRepository
                                           .Find()
                                           .FirstOrDefaultAsync(i => i.Id == _acessor.UserId)
                                           ?? throw new ArgumentException("Usuário não encontrado");

            if (!gp.Users.Contains(user)) throw new ArgumentException("Usuário não está no grupo");

            var balances = gp.Users.ToDictionary(i => i.Id, i => 0.0m);

            foreach (var expense in gp.Expenses)
            {
                if (expense.Value == 0 || expense.PaidByUserId == null) continue;

                balances[expense.PaidByUserId.Value] += expense.Value;

                if (expense.Participants == null || !expense.Participants.Any()) continue;

                var costPerPerson = expense.Value / expense.Participants.Count();

                foreach (var participant in expense.Participants)
                {
                    balances[participant.Id] -= costPerPerson;
                }
            }

            var creditors = balances.Where(b => b.Value > 0.01m).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            var debtors = balances.Where(b => b.Value < -0.01m).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            var simplifiedDebts = new List<DebtDTO>();

            while (debtors.Any() && creditors.Any())
            {
                var debtorEntry = debtors.First();
                var creditorEntry = creditors.First();

                var debtorId = debtorEntry.Key;
                var creditorId = creditorEntry.Key;

                var amountToTransfer = Math.Min(Math.Abs(debtorEntry.Value), creditorEntry.Value);

                var fromUserDto = _userMP.ToSummary(gp.Users.First(m => m.Id == debtorId));
                var toUserDto = _userMP.ToSummary(gp.Users.First(m => m.Id == creditorId));

                simplifiedDebts.Add(new DebtDTO(fromUserDto, toUserDto, amountToTransfer));

                debtors[debtorId] += amountToTransfer;
                creditors[creditorId] -= amountToTransfer;

                if (Math.Abs(debtors[debtorId]) < 0.01m)
                    debtors.Remove(debtorId);

                if (Math.Abs(creditors[creditorId]) < 0.01m)
                    creditors.Remove(creditorId);
            }

            var memberBalances = balances.Select(b => new MemberBalanceDTO(_userMP.ToSummary(gp.Users.First(m => m.Id == b.Key)), b.Value)).ToList();


            return new GroupBalanceDTO(simplifiedDebts, memberBalances);
        }

    }
}
