
using Application.DTO.User;
using Domain.Entity;

namespace Application.Interface.Mapper.UserMapper
{
    public interface IUserMP
    {

        public User ToUser(UserRegisterDTO dto, string passwordHashed);
        public User ToUser(UserResponseDTO dto);
        public User ToUser(UserSummaryDTO dto);

        public UserResponseDTO ToResponse(User user);
        public UserResponseDTO ToResponse(UserRegisterDTO dto, int id);

        public UserSummaryDTO ToSummary(User user);


    }
}
