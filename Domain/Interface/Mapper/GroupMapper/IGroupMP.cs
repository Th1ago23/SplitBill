using Domain.DTO.Group;
using Domain.Entity;
using Domain.Interface.Utils;

namespace Domain.Interface.Mapper.GroupMapper
{
    public interface IGroupMP
    {

        public Group ToEntity(GroupCreateDTO dto);

        public GroupResponseDTO ToDTO(Group gp);
    }
}
