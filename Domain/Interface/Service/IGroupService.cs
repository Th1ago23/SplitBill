using Domain.DTO.Group;

namespace Domain.Interface.Service
{
    public interface IGroupService
    {
        Task<GroupResponseDTO> GroupCreate(GroupCreateDTO dto);
        Task<GroupSummaryDTO> GetGroupWithUsers(int id);
        Task<GroupResponseDTO> UpdateGroupName(string groupName, int id);
        Task<GroupSummaryDTO> DeleteUserFromGroup(string email);

    }
}
