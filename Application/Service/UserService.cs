using BCrypt.Net;
using Domain.DTO.User;
using Domain.Interface.Context;
using Domain.Interface.Mapper.UserMapper;
using Domain.Interface.Repository;
using Domain.Interface.Service;
using Domain.Interface.Token;
using Microsoft.EntityFrameworkCore;


namespace Application.Service
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IUserMP _mapper;
        private readonly ITokenService _token;

        public UserService(ITokenService token, IUserMP mapper, IUserRepository repo)
        {
            _token = token;
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<string> Login(UserLoginDTO loginDTO)
        {
            var query = await _repo.Find().FirstOrDefaultAsync(i => i.Email.Equals(loginDTO.email));

            if (query == null) throw new Exception("E-mail ou senha inválidos.");


            bool pass = BCrypt.Net.BCrypt.Verify(loginDTO.password, query.PasswordHash);

            if (pass == false) throw new Exception("E-mail ou senha inválidos.");


            return _token.GenerateToken(query);

        }

        public async Task<UserResponseDTO> Register(UserRegisterDTO dto)
        {
            if (await _repo.Find().AnyAsync(i => i.Email == dto.EmailAddress)) throw new Exception("Esse e-mail já foi cadastrado.");

            var pass = BCrypt.Net.BCrypt.EnhancedHashPassword(dto.Password);
            
            var user = _mapper.ToUser(dto, pass);
            await _repo.Add(user);

            return new UserResponseDTO(user.Id, user.Email, user.Username);

        }
    }
}
