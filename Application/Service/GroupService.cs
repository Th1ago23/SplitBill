using Application.Service.Mapper.GroupMapper;
using Domain.DTO.Group;
using Domain.DTO.User;
using Domain.Interface.Context;
using Domain.Interface.Mapper.GroupMapper;
using Domain.Interface.Mapper.UserMapper;
using Domain.Interface.Repository;
using Domain.Interface.Service;
using Microsoft.EntityFrameworkCore;

namespace Application.Service
{
    public class GroupService:IGroupService
    {
        private readonly IGroupRepository _gr;
        private readonly IUserRepository _ur;
        private readonly ICurrentUserService _acessor;
        private readonly IGroupMP _mapper;
    
        public GroupService(IGroupMP mapper ,IGroupRepository gr, IUserRepository ur, ICurrentUserService acessor)
        {
            _mapper = mapper;
            _gr = gr;
            _ur = ur;
            _acessor = acessor;

        }

        public async Task<GroupResponseDTO> CreateGroup(GroupCreateDTO dto)
        {
            var currentUser = _acessor.UserId;

            if (currentUser == null) throw new NullReferenceException(nameof(currentUser));

            var gpzada = _mapper.ToEntity(dto);

            var user = await _ur
                                .Find()
                                .FirstOrDefaultAsync(i => i.Id == currentUser.Value)
                                ?? throw new NullReferenceException();
            
            gpzada.Users.Add(user);


            await _gr.Update(gpzada);

            return _mapper.ToDTO(gpzada);

        }

        public async Task<GroupSummaryDTO> AddMember (string gpName, string userEmail)
        {
            var gp = await _gr
                            .Find()
                            .FirstOrDefaultAsync(i => i.Name == gpName)
                            ?? throw new NullReferenceException("Grupo não encontrado.");
            var user = await _ur
                                .Find()
                                .FirstOrDefaultAsync(i => i.Email == userEmail)
                                ?? throw new NullReferenceException("Usuario não encontrado.");

            gp.Users.Add(user);

            await _gr.Update(gp);

            var membersDTOS = new List<UserSummaryDTO>();

            foreach(var u in gp.Users)
            {
                membersDTOS.Add(new UserSummaryDTO(u.FullName));
            }

            return new GroupSummaryDTO(gp.Name, membersDTOS);
        }
    }
}
