using BeajLearner.Application.Interfaces;
using System.Security.Claims;

namespace BeajLearner.WebApi.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
            FullName = httpContextAccessor.HttpContext?.User?.FindFirstValue("fullName");
        }

        public string UserId { get; }
        public string FullName { get; }
    }
}
