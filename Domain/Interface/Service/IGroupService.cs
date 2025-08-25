using Domain.DTO.Group;

namespace Domain.Interface.Service
{
    public interface IGroupService
    {

        public Task<GroupResponseDTO> CreateGroup(GroupCreateDTO dto);
        public Task<GroupSummaryDTO> AddMember(int gpId, string userEmail);
        public Task<string> GetGroupName(int id);
        
        public Task<GroupSummaryDTO> RemoveMember(int groupId, int userId);
        public Task<bool> DeleteGroup(int groupId);
        public Task<GroupSummaryDTO> RenameGroup(string name, int groupId);


    }
}
