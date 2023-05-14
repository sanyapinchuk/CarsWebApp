using System.Net.Http;
using System.Net.Http.Headers;
namespace CarsClient
{
    public static class GlobalVariables
    {
        public static HttpClient WebApiClient = new HttpClient(); // TODO dispose
        public static string IdentityServerUrl { get; set; }

        public static string Postfix = "it-car.by";

        static GlobalVariables()
        {
            WebApiClient.BaseAddress = new Uri("https://localhost:7052/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
