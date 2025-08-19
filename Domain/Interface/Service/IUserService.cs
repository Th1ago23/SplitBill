using Domain.DTO.User;

namespace Domain.Interface.Service
{
    public interface IUserService
    {
        Task<UserResponseDTO> Login(UserLoginDTO u);
        Task<UserResponseDTO> Register(UserRegisterDTO u);
    }
}
