using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Test;
using IdentityServer4.Models;
using IdentityModel;
using IdentityServer4.Test;
using System.Security.Claims;

namespace CarsIdentity
{
    public static class Configuration
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                //new ApiScope("CarsWebAPI", "Web API")
				 new ApiScope("carsApi.read", "Read Access to Cars API"),
				new ApiScope("carsApi.write", "Write Access to Cars API"),
			};

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
				new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
				new IdentityResources.Email(),
				new IdentityResource
				{
					Name = "role",
					UserClaims = new List<string> {"role"}
				}
               /* new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()*/
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
               /* new ApiResource("CarsWebAPI", "Web API", new []
                    { JwtClaimTypes.Name})
                {
                    Scopes = {"CarsWebAPI" }
                }*/
               new ApiResource
				{
					Name = "carsApi",
					DisplayName = "Cars Api",
					Description = "Allow the application to access Cars Api on your behalf",
					Scopes = new List<string> { "carsApi.read", "carsApi.write"},
					ApiSecrets = new List<Secret> {new Secret("CarApi".Sha256())},
					UserClaims = new List<string> {"role"}
				}
			};

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
				new Client
				{
					ClientId = "carsApi",
					ClientName = "cars Api",
					AllowedGrantTypes = GrantTypes.ClientCredentials,
					ClientSecrets = new List<Secret> {new Secret("CarApi".Sha256())},
					AllowedScopes = new List<string> { "carsApi.read" }
				},
				new Client
				{
					ClientId = "oidcMVCApp",
					ClientName = "Sample ASP.NET Core MVC Web App",
					ClientSecrets = new List<Secret> {new Secret("CarApi".Sha256())},

					AllowedGrantTypes = GrantTypes.Code,
					RedirectUris = new List<string> {"https://localhost:7106/signin-oidc"},
					AllowedScopes = new List<string>
					{
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile,
						IdentityServerConstants.StandardScopes.Email,
						"role",
						"carsApi.read"
					},

					RequirePkce = true,
					AllowPlainTextPkce = false
				}




                /*new Client
                {
                    ClientId = "cars-web-app",
                    ClientName = "Cars Web",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris =
                    {
						"https://fbe2-146-120-15-246.ngrok-free.app/signin-oidc"
					},
                 /*   AllowedCorsOrigins =
                    {
						"https://fbe2-146-120-15-246.ngrok-free.app"
					},
                    PostLogoutRedirectUris =
                    {
						"https://fbe2-146-120-15-246.ngrok-free.app/admin"
					},*/
                   /* AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
						IdentityServerConstants.StandardScopes.Email,
						"CarsWebAPI"
                    },
                    AllowAccessTokensViaBrowser = true
                }*/
            };

		public static List<TestUser> TestUsers =>
		
			new List<TestUser>
			{
				new TestUser
				{
					SubjectId = "56892347",
					Username = "procoder",
					Password = "password",
					Claims = new List<Claim>
					{
						new Claim(JwtClaimTypes.Email, "support@procodeguide.com"),
						new Claim(JwtClaimTypes.Role, "admin"),
						new Claim(JwtClaimTypes.WebSite, "https://procodeguide.com")
					}
				}
			};
		
	}
}
