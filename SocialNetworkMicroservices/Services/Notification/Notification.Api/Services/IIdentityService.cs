using System.Security.Claims;

namespace Notification.Api.Services
{
    public interface IIdentityService
    {
        string GetUserIdentity();
    }

    public class IdentityService : IIdentityService
    {
        private IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            _context = context;
        }

        /// <summary>
        /// "sub": "hoainam10th", user.grpc subjectId = username
        /// </summary>
        /// <returns></returns>
        public string GetUserIdentity()
        {
            return _context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
