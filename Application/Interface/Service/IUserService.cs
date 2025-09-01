using Application.DTO.User;

namespace Application.Interface.Service
{
    public interface IUserService
    {
        Task<string> Login(UserLoginDTO u);
        Task<UserRegisterResponseDTO> Register(UserRegisterDTO u);
    }
}
