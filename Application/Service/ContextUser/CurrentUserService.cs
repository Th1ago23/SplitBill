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

                if (userIdClaim != null && int.TryParse(userIdClaim, out var userId)) return userId;

                return null;
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
