using Applicaton.Common.Exceptions;

namespace CarsServer.Helpers
{
    public class AuthHelperService
    {
        public string AccessToken { get; set; }

        public AuthHelperService(IConfiguration configuration)
        {
            AccessToken = configuration["AccessToken"]!;
        }

        public void EnsureAccessTokenExists(HttpRequest request)
        {
            var header = request.Headers.FirstOrDefault(x => x.Key == "access_token");
            if (header.Value != AccessToken)
            {
                throw new UnauthorizedException();
            }
        }
    }
}
