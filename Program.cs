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
        static void Main(string[] args)
        {
            // var brands = GsmArenaApi.GetAllBrands().Result.Data;
            // var phones = new List<PhonesResponse.Phone>();
            // foreach (var brand in brands)
            // {
            //     var totalPages = 1;
            //     var currentPage = 1;
            //     do
            //     {
            //         try
            //         {
            //             var brandPhonesResponse = GsmArenaApi.GetPhonesFromTheBrand(brand.Slug, currentPage).Result;
            //             var brandPhones = brandPhonesResponse.Data.Items;
            //             totalPages = (int)brandPhonesResponse.Data.TotalPage;
            //             currentPage++;
            //             foreach (var phone in brandPhones)
            //             {
            //                 phone.Brand = brand.Name;
            //                 phones.Add(phone);
            //             }
            //         }
            //         catch (Exception e)
            //         {
            //             System.Console.WriteLine($"Brand {brand.Name} Fuckt up");
            //             System.Console.WriteLine(e.ToString());
            //         }

            //     } while (currentPage < totalPages);
            // }
            // File.WriteAllText("AllPhones.json", JsonConvert.SerializeObject(phones));
            var phonez = JsonConvert
            .DeserializeObject<List<PhonesResponse.Phone>>(File.ReadAllText("AllPhones.json"));
            File.AppendAllText("AllPhonesDetails.json", "[\n");
            var counter = 0;
            var fuckedUp = 0;
            foreach (var phone in phonez)
            {
                System.Console.Write($"{counter.ToString("D4")}/8291");
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.Write($"{(fuckedUp > 0 ? "-"+fuckedUp : "")}");
                Console.ResetColor();
                System.Console.WriteLine($"; {phone.Slug};\n{phone.Brand} - {phone.Name}");
                counter++;
                try
                {
                    var phoneDetails = GsmArenaApi.GetRawPhoneDetails(phone.Slug).Result;
                    File.AppendAllText("AllPhonesDetails.json", phoneDetails + ",\n");
                    File.AppendAllText("Succesful.json", $"{phone.Slug}; {phone.Brand} - {phone.Name}\n");
                }
                catch (System.Exception)
                {
                    fuckedUp++;
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine($"Fucked up x{fuckedUp}!!");
                    Console.ResetColor();
                    File.AppendAllText("FuckedUp.json", $"{phone.Slug}; {phone.Brand} - {phone.Name}\n");

                }
            }
            File.AppendAllText("AllPhonesDetails.json", "\n]");
        }
    }
}
