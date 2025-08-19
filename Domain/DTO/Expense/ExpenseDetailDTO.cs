using Domain.DTO.User;

namespace Domain.DTO.Expense
{
    public record ExpenseDetailDTO(string description, double value, DateTime date, UserSummaryDTO user, List<UserSummaryDTO> users)
    { }
}
