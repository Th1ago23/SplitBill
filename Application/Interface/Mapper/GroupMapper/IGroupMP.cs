
using Application.DTO.Group;
using Domain.Entity;

namespace Application.Interface.Mapper.GroupMapper
{
    public interface IGroupMP
    {
        public Group ToEntity(GroupCreateDTO dto);
        public GroupResponseDTO ToDTO(Group gp);
        
    }
}
