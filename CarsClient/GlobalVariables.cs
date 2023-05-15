using System.Net.Http;
using System.Net.Http.Headers;
namespace CarsClient
{
    public class GlobalVariables :IDisposable
    {
        public  HttpClient WebApiClient = new HttpClient(); 
        public  string IdentityServerUrl { get; set; }

        public static string Postfix = "it-car.by";

        public GlobalVariables()
        {
            WebApiClient.BaseAddress = new Uri("https://0642-146-120-15-246.ngrok-free.app/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void Dispose()
        {
            WebApiClient.Dispose();
        }
    }
}
