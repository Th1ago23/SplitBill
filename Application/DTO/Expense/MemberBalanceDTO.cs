using Application.DTO.User;

namespace Application.DTO.Expense
{
    public record MemberBalanceDTO(UserSummaryDTO user, decimal balance)
    {
    }
}
