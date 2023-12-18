using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormInstal.API
{
    public class HttpApi : HttpClient
    {
        public HttpApi()
        {
            BaseAddress = IP();
        }

        public static Uri IP()
        {
            if (Environment.MachineName.Equals("KANCELAR", StringComparison.InvariantCultureIgnoreCase))
                return new Uri("http://192.168.1.210/");
            else
                return new Uri("http://10.55.1.100/");
            //BaseAddress = new Uri("https://localhost:7208/");
        }

        public static async Task<T> DownloadFile<T>(string API = "") where T : class
        {
            //vytvožení RestAPI z nazvu třidy
            if (string.IsNullOrEmpty(API)) API = "api/" + typeof(T).ToString().Split('.').Last();

            HttpClient client = new HttpApi();
            HttpResponseMessage response = await client.GetAsync(API);
            if (response.IsSuccessStatusCode)
            {
                //obsah odpovědi převede na seznam objektů typu Trida
                var result = await response.Content.ReadAsStringAsync();
                T myData = System.Text.Json.JsonSerializer.Deserialize<T>(result);
                return myData;
            }
            return null;
        }
    }
}
