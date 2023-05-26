using Work.Rabbi.Common.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Work.Rabbi.Common.Api.Services
{
    public class LoggedinUserService : ILoggedInUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoggedinUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserId => _httpContextAccessor.HttpContext?.User.FindFirst("sub")?.Value;

        public bool IsAuthenticated => (bool)(_httpContextAccessor.HttpContext?.User.Identity.IsAuthenticated);
    }
}
