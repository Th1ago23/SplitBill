using Application.DTO.User;

namespace Application.DTO.Expense
{
    public record DebtDTO(UserSummaryDTO FromUser, UserSummaryDTO ToUser, decimal Amount)
    { }
}
