using Domain.DTO.Expense;

namespace Domain.Interface.Service
{
    public interface IExpenseService
    {
        Task<ExpenseResponseDTO> GetById(int id);
        Task<ExpenseDetailDTO> GetExpenseIdWithGroup(int id);
        Task<ExpenseDetailDTO> CreateExpense(int groupId, ExpenseCreateDTO dto);
        Task<GroupBalanceDTO> GetGroupBalanceAsync(int groupId);
    }
}
