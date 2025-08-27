using Domain.DTO.User;
using Domain.Interface.Database;
using Domain.Interface.Mapper.UserMapper;
using Domain.Interface.Repository;
using Domain.Interface.Service;
using Domain.Interface.Token;
using Microsoft.EntityFrameworkCore;


namespace Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IUserMP _mapper;
        private readonly ITokenService _token;
        private readonly IUnitOfWork _work;

        public UserService(IUnitOfWork work, ITokenService token, IUserMP mapper, IUserRepository repo)
        {
            _work = work;
            _token = token;
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<string> Login(UserLoginDTO loginDTO)
        {
            var query = await _repo.Find().FirstOrDefaultAsync(i => i.Email.Equals(loginDTO.email));

            if (query == null) throw new Exception("E-mail ou senha inválidos.");


            bool pass = BCrypt.Net.BCrypt.EnhancedVerify(loginDTO.password, query.PasswordHash);

            if (pass == false) throw new Exception("E-mail ou senha inválidos.");


            return _token.GenerateToken(query);

        }

        public async Task<UserResponseDTO> Register(UserRegisterDTO dto)
        {
            if (await _repo.Find().AnyAsync(i => i.Email == dto.EmailAddress)) throw new Exception("Esse e-mail já foi cadastrado.");
            if (string.IsNullOrWhiteSpace(dto.EmailAddress)) throw new ArgumentException("O e-mail é obrigatório.", nameof(dto.EmailAddress));
            if (dto.Password.Length < 8) throw new ArgumentException("É obrigatório a senha possuir mais de 8 caracteres");

            var pass = BCrypt.Net.BCrypt.EnhancedHashPassword(dto.Password);

            var user = _mapper.ToUser(dto, pass);
            await _repo.Add(user);
            await _work.CommitAsync();

            return new UserResponseDTO(user.Id, user.Email, user.Username);

        }
    }
}
