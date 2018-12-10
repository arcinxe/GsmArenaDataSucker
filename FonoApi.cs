using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ConsoleApp.Models;
using Newtonsoft.Json;
using QuickType;

namespace ConsoleApp
{
    public static class FonoApi
    {
        private readonly static HttpClient _client = new HttpClient();
        public static async Task<List<Dictionary<string, string>>> GetPhoneDetails(string deviceName)
        {
            var url = "https://fonoapi.freshpixl.com/v1/getdevice?" +
            "token=3ebff85ef036a160ae36245e63770363ec6921bd68d944f2" +
            $"&device={deviceName}";
            var phoneResponse = await _client.GetAsync(url);
            phoneResponse.EnsureSuccessStatusCode();
            var phoneContent = phoneResponse.Content.ReadAsStringAsync();
            var phone = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(phoneContent.Result);
            return phone;
        }

        public static void SaveAllPhoneDetails()
        {
            var phones = JsonConvert
            .DeserializeObject<List<PhonesResponse.Phone>>(File.ReadAllText("AllPhones.json"));
            File.AppendAllText("AllPhonesDetailsFonoApi.json", "[\n");
            var requestsCounter = 1;
            var fuckedUpCount = 0;
            var savedPhones = new List<Dictionary<string, string>>();
            foreach (var phone in phones)
            {
                
                System.Console.WriteLine($"{phone.Brand} - {phone.Name}");
                try
                {
                    var phonesResponse = GetPhoneDetails(phone.Name).Result;
                    // var rawPhonesDetails = JsonConvert.DeserializeObject<List<object>>(phonesResponse);
                    // var phonesDetails = JsonConvert.DeserializeObject<List<FonoApiDetails>>(phonesResponse);
                    foreach (var returnedPhone in phonesResponse)
                    {
                        System.Console.WriteLine(returnedPhone.GetValueOrDefault("DeviceName"));
                        if (savedPhones.All(p => p.GetValueOrDefault("DeviceName") != returnedPhone.GetValueOrDefault("DeviceName")))
                        {
                            savedPhones.Add(returnedPhone);
                            File.AppendAllText("AllPhonesDetailsFonoApi.json", JsonConvert.SerializeObject(returnedPhone) + ",\n");
                            File.AppendAllText("SuccesfulFonoApi.txt", $"{returnedPhone.GetValueOrDefault("DeviceName")} ; {phone.Brand} ; {phone.Name}\n");
                        }
                    }
                }
                catch (System.Exception)
                {
                    fuckedUpCount++;
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine($" Fucked up x{fuckedUpCount}!!");
                    Console.ResetColor();
                    File.AppendAllText("FuckedUpFonoApi.txt", $"{phone.Brand} ; {phone.Name}\n");

                }
                System.Console.Write($"\n{((double)requestsCounter / phones.Count * 100).ToString("0.##")}% {requestsCounter.ToString("D4")}/{phones.Count}");
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.Write($"{(fuckedUpCount > 0 ? "-" + fuckedUpCount : "")}");
                Console.ResetColor();
                Console.WriteLine($"[{savedPhones.Count}]");
                requestsCounter++;
            }
            File.AppendAllText("AllPhonesDetailsFonoApi.json", "\n]");
        }
    }
}