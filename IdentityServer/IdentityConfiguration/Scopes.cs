using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProCodeGuide.Samples.IdentityServer4.IdentityConfiguration
{
    public class Scopes
    {
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
                new ApiScope("carsApi.read", "Read Access to Cars API"),
                new ApiScope("carsApi.write", "Write Access to Cars API"),
            };
        }
    }
}
