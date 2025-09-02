using Domain.Entity;

namespace Domain.Interface.Token
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
        public string GenerateInviteToken(int userId, int groupId)
    }
}
