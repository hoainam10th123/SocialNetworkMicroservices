using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Post.Infrastructure.Data;
using Post.Infrastructure.Security;


namespace Post.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //tuong duong cai nay builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 
            services.AddHttpContextAccessor();// them dong nay neu khong loi inject HttpContextAccessor

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            //Cau hinh bai viet cua user nao thi user day moi duoc edit
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("IsPostOwner", policy =>
                {
                    policy.Requirements.Add(new IsPostOwnerRequirement());
                });

                opt.AddPolicy("IsCommentOwner", policy =>
                {
                    policy.Requirements.Add(new IsCommentOwnerRequirement());
                });
            });
            services.AddTransient<IAuthorizationHandler, IsPostOwnerRequirementHandler>();
            services.AddTransient<IAuthorizationHandler, IsCommentOwnerRequirementHandler>();

            return services;
        }
    }
}
