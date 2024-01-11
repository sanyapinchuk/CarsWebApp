using IdentityModel;
using System.Net.Http;
using System.Net.Http.Headers;
namespace CarsClient
{
    public class GlobalVariables :IDisposable
    {
        public  HttpClient WebApiClient = new HttpClient(); 
        public  string IdentityServerUrl { get; set; }

        public readonly string Postfix;
        public readonly string AppAddress;

        public GlobalVariables( IConfiguration configuration)
        {
            WebApiClient.BaseAddress = new Uri(configuration["ApiServerAddress"]);
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var temp = "CarsApi".ToSha256();

			WebApiClient.DefaultRequestHeaders.Add("secretToken", temp);

            Postfix = configuration["AppDomain"];
            AppAddress = $"https://{configuration["AppDomain"]}/";
        }

        public void Dispose()
        {
            WebApiClient.Dispose();
        }
    }
}
