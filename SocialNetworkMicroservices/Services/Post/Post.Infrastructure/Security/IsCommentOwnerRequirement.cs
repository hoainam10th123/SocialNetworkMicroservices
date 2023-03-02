using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Post.Infrastructure.Data;
using System.Security.Claims;

namespace Post.Infrastructure.Security
{
    public class IsCommentOwnerRequirement : IAuthorizationRequirement { }

    public class IsCommentOwnerRequirementHandler : AuthorizationHandler<IsCommentOwnerRequirement>
    {
        private readonly DataContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IsCommentOwnerRequirementHandler(DataContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsCommentOwnerRequirement requirement)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId)) return Task.CompletedTask;

            var commentId = _httpContextAccessor.HttpContext?.Request.RouteValues
                .SingleOrDefault(x => x.Key == "id").Value?.ToString();

            var comment = _dbContext.Comments!
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Username == userId && x.Id.ToString() == commentId).Result;

            if (comment != null)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
