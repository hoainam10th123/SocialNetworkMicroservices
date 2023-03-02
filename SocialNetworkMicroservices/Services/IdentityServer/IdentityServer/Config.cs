using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using IdentityServer4;
using System.Security.Claims;

namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "movies_mvc_client",
                    ClientName = "Movies MVC Web App",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RequirePkce = false,
                    AllowRememberConsent = false,
                    RedirectUris = new List<string>()
                    {
                        "https://localhost:5002/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:5002/signout-callback-oidc"
                    },
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        "postAPI",
                        "roles"
                    }
                },
                //Block 3: SPA client using Code flow
                new Client
                {
                    ClientId = "angularClient",
                    ClientName = "Angular SPA Client",
                    ClientUri = "http://localhost:4200",
                    RequireConsent = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris =
                    {
                        "http://localhost:4200",
                        "http://localhost:4200/auth-callback",
                        "http://localhost:4200/silent-renew.html"
                    },
                    PostLogoutRedirectUris = new List<string>() { "http://localhost:4200" },
                    AllowedCorsOrigins = { "http://localhost:4200" },
                    AllowedScopes = new List<string>()
                    { 
                        "openid", 
                        "profile", 
                        "address", 
                        "email", 
                        "roles", 
                        "userAPI", 
                        "postAPI", 
                        "notificationAPI", 
                        "chatAPI" 
                    }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
           new ApiScope[]
           {
               new ApiScope("userAPI", "User API"),
               new ApiScope("postAPI", "Post API"),
               new ApiScope("notificationAPI", "Notification API"),
               new ApiScope("chatAPI", "Chat API")
           };

        public static IEnumerable<ApiResource> ApiResources =>
          new ApiResource[]
          {
               new ApiResource("userAPI", "User API"),
               new ApiResource("postAPI", "Post API"),
               new ApiResource("notificationAPI", "Notification API"),
               new ApiResource("chatAPI", "Chat API")
          };

        public static IEnumerable<IdentityResource> IdentityResources =>
          new IdentityResource[]
          {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile(),
              new IdentityResources.Address(),
              new IdentityResources.Email(),
              new IdentityResource(
                    "roles",
                    "Your role(s)",
                    new List<string>() { "role" })
          };

        public static List<TestUser> TestUsers =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1234",
                    Username = "hoainam10th",
                    Password = "123456",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.GivenName, "Nam"),
                        new Claim(JwtClaimTypes.FamilyName, "Nguyen")
                    }
                }
            };
    }
}
