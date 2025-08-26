using Domain.DTO.Group;
using Domain.DTO.User;

namespace Domain.DTO.Expense
{
    public record ExpenseDetailDTO(string description, decimal value, DateTime date, UserSummaryDTO user, List<UserSummaryDTO> users, GroupResponseDTO dto)
    { }
}
