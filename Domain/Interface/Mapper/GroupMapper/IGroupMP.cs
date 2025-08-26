using Domain.DTO.Group;
using Domain.Entity;

namespace Domain.Interface.Mapper.GroupMapper
{
    public interface IGroupMP
    {
        public Group ToEntity(GroupCreateDTO dto);
        public GroupResponseDTO ToDTO(Group gp);
        public GroupSummaryDTO ToSummary(Group gp);
    }
}
