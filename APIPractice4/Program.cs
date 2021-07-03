using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;


namespace APIPractice4
{
    public class DataObject
    {
        public string Name { get; set; }
    }

    class Program
    {
        //Global:
        private const string URL = "https://www.dnd5eapi.co/api/";
        private string urlParameters;
        static void Main(string[] args)
        {
            MakeRequest("spells/acid-arrow/");
        }

        static void MakeRequest(string urlParams)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(urlParams).Result;
            if (response.IsSuccessStatusCode)
            {
                //FAIL: cannot deserialize JSON as IEnumerable
                var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;
                foreach(var d in dataObjects)
                {
                    Console.WriteLine("{0}", d.Name);
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }
    }
}
