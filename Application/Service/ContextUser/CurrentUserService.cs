#pragma warning disable IDE0290
using Domain.Entity;
using Domain.Interface.Context;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Service.ContextUser
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _acessor;

        public CurrentUserService(IHttpContextAccessor acessor)
        {
            _acessor = acessor;
        }

        public int? UserId
        {
            get
            {
                var userIdClaim = _acessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userIdClaim))
                    throw new UnauthorizedAccessException("Usuário não autenticado ou token inválido.");

                if (!int.TryParse(userIdClaim, out int userId))
                    throw new UnauthorizedAccessException("Claim de usuário inválida no token.");

                return userId;
            }
        }

        public string? UserEmail
        {
            get
            {
                return _acessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
            }
        }
    }
}
