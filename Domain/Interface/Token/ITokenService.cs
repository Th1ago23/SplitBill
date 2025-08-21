using Domain.Entity;

namespace Domain.Interface.Token
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        //User CurrentUserService();
    }
}
