using System;
using Newtonsoft;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using ConsoleApp.Models;
using System.Linq;

namespace Documents
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();
            var brandsResponse = await client.GetAsync("http://localhost:8080/brands");
            brandsResponse.EnsureSuccessStatusCode();
            var brandsContent = brandsResponse.Content.ReadAsStringAsync();
            // Console.WriteLine(content.Result);
            var brands = JsonConvert.DeserializeObject<BrandsResponse>(brandsContent.Result);
            // Console.WriteLine("Hello World!");
            // System.Console.WriteLine(brands.Data.FirstOrDefault().Slug);
            File.WriteAllText("Brands.txt", JsonConvert.SerializeObject(brands));
            var phonesResponse = await client.GetAsync($"http://localhost:8080/devices/{brands.Data.FirstOrDefault().Slug}?page=1");
            var phonesContent = phonesResponse.Content.ReadAsStringAsync();
            var phones = JsonConvert.DeserializeObject<PhonesResponse>(phonesContent.Result);
            File.WriteAllText("Phones.txt", JsonConvert.SerializeObject(phones));
            // System.Console.WriteLine(brands.Data.Sum(b => b.NumberOfDevices));
        }
    }
}
