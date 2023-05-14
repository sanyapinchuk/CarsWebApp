using IdentityModel;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProCodeGuide.Samples.IdentityServer4.IdentityConfiguration
{
    public class Users
    {
        public static List<TestUser> Get()
        {
            return new List<TestUser> 
            {
                new TestUser 
                {
                    SubjectId = "56892347",
                    Username = "admin",
                    Password = "admin",
                    Claims = new List<Claim> 
                    {
                        new Claim(JwtClaimTypes.Email, "support@itcar.by"),
                        new Claim(JwtClaimTypes.Role, "admin"),
                        new Claim(JwtClaimTypes.WebSite, "https://it-car.by")
                    }
                }
            };
        }
    }
}
