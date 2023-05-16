using IdentityModel;
using System.Net.Http;
using System.Net.Http.Headers;
namespace CarsClient
{
    public class GlobalVariables :IDisposable
    {
        public  HttpClient WebApiClient = new HttpClient(); 
        public  string IdentityServerUrl { get; set; }

        public static string Postfix = "it-car.by";

        public GlobalVariables( IConfiguration configuration)
        {
            WebApiClient.BaseAddress = new Uri(configuration["ApiServerAddress"]);
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var temp = "CarsApi".ToSha256();

			WebApiClient.DefaultRequestHeaders.Add("secretToken", temp);
		}

        public void Dispose()
        {
            WebApiClient.Dispose();
        }
    }
}
