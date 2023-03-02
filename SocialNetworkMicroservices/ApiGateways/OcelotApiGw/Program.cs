using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOcelot();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:4200")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod()
                                      .AllowCredentials();
                      });
});

var app = builder.Build();

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);
});

app.UseCors(MyAllowSpecificOrigins);

app.MapGet("/", () => "Hello World!");
// for signalR
app.UseWebSockets();
await app.UseOcelot();
app.Run();
