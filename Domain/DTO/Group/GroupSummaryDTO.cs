using Domain.DTO.Expense;
using Domain.DTO.User;

namespace Domain.DTO.Group
{
    public record GroupSummaryDTO(string name, int leaderId, ICollection<UserSummaryDTO> users, bool isPublic, ICollection<ExpenseDetailDTO> Expenses)
    { }
}
