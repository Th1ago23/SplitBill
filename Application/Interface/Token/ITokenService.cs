using Domain.Entity;
using Domain.Helpers;
using System.Security.Claims;

namespace Domain.Interface.Token
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
        public string GenerateInviteToken(Invite invite);
        public ClaimsPrincipal ValidateToken (string token);
    }
}
