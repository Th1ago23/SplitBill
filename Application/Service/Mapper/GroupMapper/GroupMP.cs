

using Domain.DTO.Group;
using Domain.Entity;
using Domain.Interface.Mapper.GroupMapper;
using Domain.Interface.Mapper.UserMapper;
using Domain.Interface.Utils;
using System.Reflection.Metadata.Ecma335;

namespace Application.Service.Mapper.GroupMapper
{
    public class GroupMP:IGroupMP
    {
        private readonly IObjectValidator _validator;
        private readonly IUserMP _userMapper;

        public GroupMP (IObjectValidator validator, IUserMP userMapper)
        {
            _validator = validator;
            _userMapper = userMapper;
        }

        public Group ToEntity (GroupCreateDTO dto)
        {
            _validator.Validate(dto);

            return new Group
            {
                Name = dto.name
            };
        }

        public GroupResponseDTO ToDTO (Group gp)
        {
            _validator.Validate(gp);
            return new GroupResponseDTO(gp.Name, gp.LeaderId, gp.IsPublic);
        }



        public GroupSummaryDTO ToSummary(Group gp)
        {
            _validator.Validate(gp);

            var membersDTO = gp.Users
                                    .Select(_userMapper.ToSummary).ToList();

            return new GroupSummaryDTO(gp.Name,gp.LeaderId, membersDTO, gp.IsPublic);
        }


        
    }
}
