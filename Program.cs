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
            var phonez =  JsonConvert.DeserializeObject<List<PhonesResponse.Phone>>(File.ReadAllText("AllPhones.json"));
            System.Console.WriteLine(phonez.Count);
            return;
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
    }
}
