using Application.DTO.Group;
using Application.DTO.User;

namespace Application.DTO.Expense
{
    public record ExpenseDetailDTO(int id, string description, decimal value, DateTime date, UserSummaryDTO user, List<UserSummaryDTO> users, GroupResponseDTO dto)
    { }
}
