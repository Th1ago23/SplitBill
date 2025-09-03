using Application.DTO.User;
using Application.Interface.Service;
using Domain.Interface.Token;
using Microsoft.AspNetCore.Mvc;

namespace SplitBill.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _service;
    private readonly ITokenService _token;

    public AuthController(ITokenService token, IUserService service)
    {
        _token = token;
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
        try
        {
            if (request == null) return BadRequest("Preencha todos os dados");

            var response = await _service.Register(request);

            return Ok(response);
        }

        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return Conflict(new { error = ex.Message });
        }
    }
}

