using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notification.Application.Contracts.Persistence;
using Notification.Infrastructure.Persistence;
using Notification.Infrastructure.Repositories;


namespace Notification.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseMySQL(configuration.GetConnectionString("DefaultConnection")) );

            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<INotiRepository, NotiRepository>();

            //services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            //services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
