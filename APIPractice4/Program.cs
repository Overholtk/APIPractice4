using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace APIPractice4
{
    public class DataObject
    {
        public string Index { get; set; }
        public string Name { get; set; }
        public List<string> Desc { get; set; }
    }

    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("https://www.dnd5eapi.co/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var data = await GetDataAsync("spells/acid-arrow/");
                Console.Write($"{data.Index}, {data.Name}, ");
                foreach(string d in data.Desc)
                {
                    Console.Write($"{d} ");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static async Task<DataObject> GetDataAsync(string path)
        {
            DataObject data = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadAsAsync<DataObject>();
            }

            return data;
        }
    }
}
