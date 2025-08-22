using Domain.DTO.Group;

namespace Domain.Interface.Service
{
    public interface IGroupService
    {

        public Task<GroupResponseDTO> CreateGroup(GroupCreateDTO dto);
        public Task<GroupSummaryDTO> AddMember(string gpName, string userEmail);


    }
}
