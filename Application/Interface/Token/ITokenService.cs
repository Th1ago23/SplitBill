using Domain.Entity;
using Domain.Helpers;

namespace Domain.Interface.Token
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
        public string GenerateInviteToken(Invite invite);
    }
}
