
using Domain.Helpers;
using Domain.Interface.Context;
using Domain.Interface.Repository;
using Domain.Interface.Token;
using Microsoft.EntityFrameworkCore;

namespace Application.Service
{
    public class InvitationService
    {
        private readonly ITokenService _token;
        private readonly ICurrentUserService _user;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;

        public InvitationService(IUserRepository userRepository, IGroupRepository groupRepository, ICurrentUserService user, ITokenService token)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _user = user;
            _token = token;
        }

        public async Task<string> CreateInvite (int groupId, int userId)
        {
            var gp = await _groupRepository.Find().FirstOrDefaultAsync(i => i.Id == groupId) ?? throw new NullReferenceException("Grupo não encontrado");
            var user = await _userRepository.Find().FirstOrDefaultAsync(i => i.Id == userId) ?? throw new NullReferenceException("Usuário não econtrado");

            if (!gp.IsPublic && gp.LeaderId != user.Id) throw new UnauthorizedAccessException("Sem permissão para convidar outros usuários");


            var obj = new Invite(Guid.NewGuid(), gp.Id, user.Id, DateTime.UtcNow.AddDays(1));

            var token = _token.GenerateInviteToken(userId, groupId);
            
        }
    }
}
