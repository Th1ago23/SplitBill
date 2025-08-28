using Domain.DTO.User;
using Domain.Interface.Service;
using Microsoft.AspNetCore.Mvc;

namespace SplitBill.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;

        public AuthController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDTO request)
        {
            if (request == null) return BadRequest("É necessário inserir todos os dados.");

            var token = await _service.Login(request);

            return Ok(new { token });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDTO request)
        {
            if (request == null) return BadRequest("Preencha todos os dados");
            await _service.Register(request);

            return CreatedAtAction(
                "Register",
                new { message = "Usuário criado com Sucesso.",
                    data = request
                    }
                );
        }
    }
}