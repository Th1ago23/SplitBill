
using Application.Utils.Validator;
using Domain.DTO.User;
using Domain.Entity;
using Domain.Interface.Mapper.UserMapper;
using Domain.Interface.Utils;

namespace Application.Service.Mapper.UserMapper
{
    public class UserMP:IUserMP
    {
        private readonly IObjectValidator _validator;

        public UserMP(IObjectValidator validator)
        {
            _validator = validator;
        }

        public User ToUser(UserResponseDTO dto)
        {
            _validator.Validate(dto);

            return new User
            {
                Id = dto.id,
                Email = dto.email,
                Username = dto.username,
            };
        }

        public User ToUser(UserRegisterDTO dto, string passwordHashed)
        {
            _validator.Validate(dto);

            return new User(dto.Fullname,dto.Username,dto.EmailAddress,passwordHashed,dto.BirthDay);
        }

        public User ToUser(UserSummaryDTO dto)
        {
            _validator.Validate(dto);

            return new User
            {
                FullName = dto.Name
            }; 
        }

        public UserResponseDTO ToResponse (User user)
        {
            _validator.Validate(user);

            return new UserResponseDTO(user.Id, user.Email, user.Username);
        }

        public UserResponseDTO ToResponse(UserRegisterDTO dto, int id)
        {
            _validator.Validate(dto);

            return new UserResponseDTO(id, dto.EmailAddress, dto.Username);
        }

        public UserSummaryDTO ToSummary(User user)
        {
            _validator.Validate(user);
            return new UserSummaryDTO(user.FullName);
        }


    }
}
