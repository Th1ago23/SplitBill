using Domain.DTO.User;

namespace Domain.Interface.Service
{
    public interface IUserService
    {
        Task<string> Login(UserLoginDTO u);
        Task<UserResponseDTO> Register(UserRegisterDTO u);
    }
}
