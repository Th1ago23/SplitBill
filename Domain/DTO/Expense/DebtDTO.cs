using Domain.DTO.User;

namespace Domain.DTO.Expense
{
    public record DebtDTO(UserSummaryDTO FromUser, UserSummaryDTO ToUser, decimal Amount)
    { }
}
