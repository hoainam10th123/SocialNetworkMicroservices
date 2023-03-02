using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Post.Infrastructure.Data;
using System.Security.Claims;

namespace Post.Infrastructure.Security
{
    //Microsoft.AspNetCore.Authorization
    public class IsPostOwnerRequirement : IAuthorizationRequirement { }

    public class IsPostOwnerRequirementHandler : AuthorizationHandler<IsPostOwnerRequirement>
    {
        private readonly DataContext _dbContext;
        // Microsoft.AspNetCore.Http.Abstractions
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IsPostOwnerRequirementHandler(DataContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsPostOwnerRequirement requirement)
        {
            var username = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(username)) return Task.CompletedTask;

            //Microsoft.AspNetCore.Authentication.JwtBearer: RouteValues
            var postSlug = _httpContextAccessor.HttpContext.Request.RouteValues
                .SingleOrDefault(x => x.Key == "id").Value.ToString();

            var post = _dbContext.Posts
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Username == username && x.Id.ToString() == postSlug).Result;

            if (post != null)
                context.Succeed(requirement);
            
            return Task.CompletedTask;
        }
    }
}
