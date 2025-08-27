using Domain.DTO.User;

namespace Domain.DTO.Expense
{
    public record MemberBalanceDTO(UserSummaryDTO user, decimal balance)
    {
    }
}
