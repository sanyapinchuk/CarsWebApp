using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityModel;

namespace CarsIdentity
{
    public static class Configuration
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("CarsWebAPI", "Web API")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("CarsWebAPI", "Web API", new []
                    { JwtClaimTypes.Name})
                {
                    Scopes = {"CarsWebAPI" }
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "cars-web-app",
                    ClientName = "Cars Web",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris =
                    {
						"https://localhost:7106/signin-oidc"
					},
                    AllowedCorsOrigins =
                    {
						"https://localhost:7106"
					},
                    PostLogoutRedirectUris =
                    {
						"https://localhost:7106/signout-oidc"
					},
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "CarsWebAPI"
                    },
                    AllowAccessTokensViaBrowser = true
                }
            };
    }
}
