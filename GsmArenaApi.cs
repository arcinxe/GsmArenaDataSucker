using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            var brandCount = 1;

            foreach (var brand in brands)
            {
                System.Console.WriteLine($"{brandCount++}/{brands.Length} ");

                var phoneCount = 0;
                var totalPages = 0;
                var currentPage = 1;
                do
                {
                    try
                    {
                        var brandPhonesResponse = GsmArenaApi.GetPhonesFromTheBrand(brand.Slug, currentPage).Result;
                        var brandPhones = brandPhonesResponse.Data.Items;
                        totalPages = (int)brandPhonesResponse.Data.TotalPage;
                        System.Console.WriteLine($"{brand.Name} ({currentPage} page out of {totalPages})");
                        currentPage++;
                        foreach (var phone in brandPhones)
                        {
                            phone.Brand = brand.Name;
                            phones.Add(phone);
                            phoneCount++;
                        }
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine($"Brand {brand.Name} Failed");
                        System.Console.WriteLine(e.ToString());
                    }

                } while (currentPage <= totalPages);
                System.Console.WriteLine($"Total amount of {brand.Name} devices: {phoneCount}");
            }

            File.WriteAllText("AllPhones.json", JsonConvert.SerializeObject(phones.OrderByDescending(p => p.Id), Formatting.Indented));
        }

        public static void SaveAllPhonesDetails()
        {
            File.Delete("AllPhonesDetails.json");
            File.Delete("Succesful.txt");
            File.Delete("Failed.txt");

            var phones = JsonConvert
            .DeserializeObject<List<PhonesResponse.Phone>>(File.ReadAllText("AllPhones.json"));
            var startTime = DateTime.Now;
            File.AppendAllText($"AllPhonesDetails_{startTime.ToString("yyyy-MM-dd")}.json", "[\n");
            var counter = 0;
            var failed = 0;
            var succesfull = 0;

            foreach (var phone in phones)
            {
                System.Console.Write($"{counter.ToString("D4")}/{phones.Count}");
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.Write($"{(failed > 0 ? "-" + failed : "")}");
                Console.ResetColor();
                System.Console.Write($", {((double)counter/phones.Count).ToString("P")}");
                System.Console.WriteLine($", {phone.Slug};\n{phone.Brand} - {phone.Name}");
                counter++;
                try
                {
                    var phoneDetails = GsmArenaApi.GetRawPhoneDetails(phone.Slug).Result;
                    File.AppendAllText($"AllPhonesDetails_{startTime.ToString("yyyy-MM-dd")}.json", (succesfull++ > 0 ? "\n," : "") + phoneDetails.Insert(9, $"\"id\":\"{phone.Id}\",\"slug\":\"{phone.Slug}\",")/*  + ",\n" */);
                    File.AppendAllText("Succesful.txt", $"{phone.Slug}; {phone.Brand} - {phone.Name}\n");
                }
                catch (System.Exception)
                {
                    failed++;
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine($"Failed x{failed}!!");
                    Console.ResetColor();
                    File.AppendAllText("Failed.txt", $"{phone.Slug}; {phone.Brand} - {phone.Name}\n");

                }
            }
            File.AppendAllText($"AllPhonesDetails_{DateTime.Now.ToString("yyyy-MM-dd")}.json", "\n]");
            System.Console.WriteLine($"gud: {succesfull} devices");

        }
    }
}