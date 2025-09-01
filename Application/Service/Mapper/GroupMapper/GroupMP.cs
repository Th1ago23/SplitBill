

using Application.DTO.Group;
using Application.Interface.Mapper.ExpenseMapper;
using Application.Interface.Mapper.GroupMapper;
using Domain.Entity;
using Domain.Interface.Utils;

namespace Application.Service.Mapper.GroupMapper
{
    public class GroupMP : IGroupMP
    {
        private readonly IObjectValidator _validator;

        public GroupMP(IExpenseMP expenseMP, IObjectValidator validator)
        {
            _validator = validator;
        }

        public Group ToEntity(GroupCreateDTO dto)
        {
            _validator.Validate(dto);

            return new Group
            {
                Name = dto.name
            };
        }

        public GroupResponseDTO ToDTO(Group gp)
        {
            _validator.Validate(gp);
            return new GroupResponseDTO(gp.Id,gp.Name, gp.LeaderId, gp.IsPublic);
        }
    }
}
