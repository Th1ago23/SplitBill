

using Domain.DTO.Expense;
using Domain.Interface.Context;
using Domain.Interface.Database;
using Domain.Interface.Mapper.ExpenseMapper;
using Domain.Interface.Repository;
using Domain.Interface.Service;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Application.Service
{
    public class ExpenseService
    {
        private readonly IExpenseRepository _context;
        private readonly IExpenseMP _mapper;
        private readonly IUnitOfWork _work;
        private readonly ICurrentUserService _acessor;
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;

        public ExpenseService(IUserRepository userRepository, IGroupRepository groupRepository, ICurrentUserService acessor, IUnitOfWork work, IExpenseMP mapper, IExpenseRepository context)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _work = work;
            _acessor = acessor;
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExpenseResponseDTO> GetById (int id)
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

            return _mapper.ToDetails(response);
        }

        public async Task<ExpenseDetailDTO> CreateExpense(int groupId, ExpenseCreateDTO dto)
        {
            var user = await _userRepository
                                        .Find()
                                        .FirstOrDefaultAsync(i=> i.Id == _acessor.UserId)
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

            return _mapper.ToDetails(newExpense);
        }

        public async Task<GroupBalanceDTO> GetGroupBalanceAsync(int groupId)
        {
            var currentUserId = _acessor.UserId ?? throw new NullReferenceException("Usuário não autenticado");
        }

    }
}
