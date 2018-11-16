using System;
using Newtonsoft;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using ConsoleApp.Models;
using System.Linq;
using ConsoleApp;
using System.Collections.Generic;

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
            File.WriteAllText("PhonesResponse.txt", JsonConvert.SerializeObject(phones));
            // System.Console.WriteLine(brands.Data.Sum(b => b.NumberOfDevices));
            // System.Console.WriteLine(GsmArenaApi.GetPhoneDetails(GsmArenaApi.GetPhonesFromTheBrand(GsmArenaApi.GetAllBrands().Result.Data.FirstOrDefault().Slug).Result.Data.Items.FirstOrDefault().Slug).Result.Data.Detail.Display.Resolution);
            System.Console.WriteLine(GsmArenaApi.GetPhonesFromTheBrand(GsmArenaApi.GetAllBrands().Result.Data.FirstOrDefault().Slug).Result.Data.Items.FirstOrDefault().Slug);
            var foo = GsmArenaApi.GetPhoneDetails("acer_chromebook_tab_10-9139").Result;
            System.Console.WriteLine(foo.Data.Detail.Display.Resolution);
            // var phones = new List<PhonesResponse.Phone>();

        }
    }
}
