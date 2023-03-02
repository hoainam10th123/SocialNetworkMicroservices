using User.Grpc.Data;
using User.Grpc.Dtos;
using User.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("UserStoreDatabase"));

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddScoped<IUserContext, UserContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<UserService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

using (var scope = app.Services.CreateScope())
{
    try
    {
        var userService = scope.ServiceProvider.GetRequiredService<IUserContext>();
        await Seed.SeedData(userService.Users, builder.Configuration);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred during migration");
    }
}

app.Run();
