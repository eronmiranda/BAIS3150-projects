using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BAIS3150_Service_API_Assignment
{
    class Program
    {
        static async Task Main(string[] args)
        {
            {
                using(HttpClient WebApiClient = new HttpClient())
                {
                    MediaTypeWithQualityHeaderValue ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                    WebApiClient.DefaultRequestHeaders.Accept.Add(ContentType);

                    WebApiClient.BaseAddress = new Uri("https://lcoalhost:44334");

                    string WebAPIGetContent;
                    HttpResponseMessage WebApiResponseMessage;

                    WebApiResponseMessage = await WebApiClient.GetAsync("/GetCustomers");
                    WebAPIGetContent = await WebApiResponseMessage.Content.ReadAsStringAsync();
                    Console.ReadKey();
                }
            }
        }
    }
}
