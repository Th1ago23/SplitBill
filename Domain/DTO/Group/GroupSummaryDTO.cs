using Domain.DTO.User;

namespace Domain.DTO.Group
{
    public record GroupSummaryDTO(string name, List<UserSummaryDTO> users)
    { }
}
