using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using ConsoleApp.Models;
using Newtonsoft.Json;

namespace ConsoleApp
{
    public static class GsmArenaApi
    {
        private readonly static HttpClient _client = new HttpClient();
        public static async Task<BrandsResponse> GetAllBrands()
        {
            var brandsResponse = await _client.GetAsync("http://localhost:8080/brands");
            brandsResponse.EnsureSuccessStatusCode();
            var brandsContent = brandsResponse.Content.ReadAsStringAsync();
            var brands = JsonConvert.DeserializeObject<BrandsResponse>(brandsContent.Result);
            return brands;
        }

        public static async Task<PhonesResponse> GetPhonesFromTheBrand(string slug, int page = 1)
        {
            var phonesResponse = await _client.GetAsync($"http://localhost:8080/devices/{slug}?page={page}");
            phonesResponse.EnsureSuccessStatusCode();
            var phonesContent = phonesResponse.Content.ReadAsStringAsync();
            var phones = JsonConvert.DeserializeObject<PhonesResponse>(phonesContent.Result);
            return phones;
        }

        public static async Task<PhoneDetailsResponse> GetPhoneDetails(string slug)
        {
            var phoneResponse = await _client.GetAsync($"http://localhost:8080/specs/{slug}");
            phoneResponse.EnsureSuccessStatusCode();
            var phoneContent = phoneResponse.Content.ReadAsStringAsync();
            var phone = JsonConvert.DeserializeObject<PhoneDetailsResponse>(phoneContent.Result);
            return phone;
        }

        public static async Task<string> GetRawPhoneDetails(string slug)
        {
            var phoneResponse = await _client.GetAsync($"http://localhost:8080/specs/{slug}");
            phoneResponse.EnsureSuccessStatusCode();
            var phoneContent = phoneResponse.Content.ReadAsStringAsync();
            return phoneContent.Result;
        }

        public static void SaveAllPhonesList()
        {
            var brands = GsmArenaApi.GetAllBrands().Result.Data;
            var phones = new List<PhonesResponse.Phone>();
            foreach (var brand in brands)
            {
                var totalPages = 1;
                var currentPage = 1;
                do
                {
                    try
                    {
                        var brandPhonesResponse = GsmArenaApi.GetPhonesFromTheBrand(brand.Slug, currentPage).Result;
                        var brandPhones = brandPhonesResponse.Data.Items;
                        totalPages = (int)brandPhonesResponse.Data.TotalPage;
                        currentPage++;
                        foreach (var phone in brandPhones)
                        {
                            phone.Brand = brand.Name;
                            phones.Add(phone);
                        }
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine($"Brand {brand.Name} Fuckt up");
                        System.Console.WriteLine(e.ToString());
                    }

                } while (currentPage < totalPages);
            }
            File.WriteAllText("AllPhones.json", JsonConvert.SerializeObject(phones));
        }

        public static void SaveAllPhonesDetails()
        {
            var phones = JsonConvert
            .DeserializeObject<List<PhonesResponse.Phone>>(File.ReadAllText("AllPhones.json"));
            File.AppendAllText("AllPhonesDetails.json", "[\n");
            var counter = 0;
            var fuckedUp = 0;
            foreach (var phone in phones)
            {
                System.Console.Write($"{counter.ToString("D4")}/8291");
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.Write($"{(fuckedUp > 0 ? "-" + fuckedUp : "")}");
                Console.ResetColor();
                System.Console.WriteLine($"; {phone.Slug};\n{phone.Brand} - {phone.Name}");
                counter++;
                try
                {
                    var phoneDetails = GsmArenaApi.GetRawPhoneDetails(phone.Slug).Result;
                    File.AppendAllText("AllPhonesDetails.json", phoneDetails + ",\n");
                    File.AppendAllText("Succesful.txt", $"{phone.Slug}; {phone.Brand} - {phone.Name}\n");
                }
                catch (System.Exception)
                {
                    fuckedUp++;
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine($"Fucked up x{fuckedUp}!!");
                    Console.ResetColor();
                    File.AppendAllText("FuckedUp.txt", $"{phone.Slug}; {phone.Brand} - {phone.Name}\n");

                }
            }
            File.AppendAllText("AllPhonesDetails.json", "\n]");
        }
    }
}