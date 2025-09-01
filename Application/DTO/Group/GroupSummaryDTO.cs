using Application.DTO.Expense;
using Application.DTO.User;

namespace Application.DTO.Group
{
    public record GroupSummaryDTO(int id, string name, int leaderId, ICollection<UserSummaryDTO> users, bool isPublic, ICollection<ExpenseDetailDTO> Expenses)
    { }
}
