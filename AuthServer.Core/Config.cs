using System.Collections.Generic;
using AuthServer.Core.Models;
using IdentityServer4;
using IdentityServer4.Models;

namespace AuthServer.Core
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            //Setup API to be protected
            return new List<ApiResource>
            {
                new ApiResource("api1")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api1" }
                },
                //MVC Client
                new Client
                {
                    ClientId = "MVC",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RequireClientSecret = true,
                    RequireConsent = false,
                    RedirectUris = { "http://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email
                    },
                    //AlwaysIncludeUserClaimsInIdToken = true

                },
                //or Angular Client
                new Client {
                    ClientId = "angular_spa",
                    ClientName = "Angular 4 Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = new List<string> { "openid", "profile", "api1" },
                    RedirectUris = new List<string> { "http://localhost:5003/auth-callback" },
                    PostLogoutRedirectUris = new List<string> { "http://localhost:5003/" },
                    AllowedCorsOrigins = new List<string> { "http://localhost:5003" },
                    RequireConsent = false,
                    AllowAccessTokensViaBrowser = true
                },
                //Winforms Client
                //Android Client
                new Client
                {
                    ClientId = "AuthServer.Android",
                    ClientName = "Android Client",
                    ClientSecrets =
                    {
                        new Secret("myClientSecret".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = new List<string> { "openid", "profile", "api1" },
                    RedirectUris = new List<string> { "com.authserver.android://callback" },
                    RequireConsent = false
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        public static IEnumerable<ApplicationUser> GetUsers()
        {
            return new List<ApplicationUser>
            {
                new ApplicationUser("Admin"),
                new ApplicationUser("Customer1"),
                new ApplicationUser("Customer2"),
                new ApplicationUser("Employee")
            };
        }
    }
}
