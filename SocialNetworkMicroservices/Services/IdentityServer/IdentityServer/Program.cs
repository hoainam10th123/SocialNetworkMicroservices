using IdentityServer;
using IdentityServer.GrpcService;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication.Cookies;
using User.Grpc.Protos;

//string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// them dong nay
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

/*builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                      });
});*/

//GET: http://localhost:5077/.well-known/openid-configuration
// khi run se tu sinh ra file tempkey.jwk
builder.Services.AddIdentityServer()
    .AddInMemoryClients(Config.Clients)
    .AddInMemoryApiResources(Config.ApiResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryIdentityResources(Config.IdentityResources)
//.AddTestUsers(Config.TestUsers)
.AddDeveloperSigningCredential();

// login redict to angular
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

/*builder.Services.Configure<CookieAuthenticationOptions>(IdentityServerConstants.DefaultCookieAuthenticationScheme, options =>
{
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.IsEssential = true;
});*/

builder.Services.AddScoped<UserGrpcService>();
builder.Services.AddGrpcClient<UserProtoService.UserProtoServiceClient>
    (o => o.Address = new Uri(builder.Configuration["GrpcSettings:UserUrl"]));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();// bootstrap.css

//app.UseCors(MyAllowSpecificOrigins);

// them dong nay
app.UseRouting();
app.UseIdentityServer();
//app.UseAuthentication();
app.UseAuthorization();// required login

// them dong nay
app.MapRazorPages();
app.MapDefaultControllerRoute();

//app.MapGet("/", () => "Hello World!");

app.Run();
//powershell: iex ((New-Object System.Net.WebClient).DownloadString('https://raw.githubusercontent.com/IdentityServer/IdentityServer4.Quickstart.UI/main/getmain.ps1'))
