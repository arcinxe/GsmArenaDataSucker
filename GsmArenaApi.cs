using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GsmArenaDataSucker.Models;
using Newtonsoft.Json;

namespace GsmArenaDataSucker {
    public static class GsmArenaApi {
        private readonly static HttpClient _client = new HttpClient();
        public static async Task<BrandsResponse> GetAllBrands() {
            var brandsResponse = await _client.GetAsync("http://localhost:8080/brands");
            brandsResponse.EnsureSuccessStatusCode();
            var brandsContent = brandsResponse.Content.ReadAsStringAsync();
            var brands = JsonConvert.DeserializeObject<BrandsResponse>(brandsContent.Result);
            return brands;
        }

        public static async Task<DevicesResponse> GetDevicesFromTheBrand(string slug, int page = 1) {
            var devicesResponse = await _client.GetAsync($"http://localhost:8080/devices/{slug}?page={page}");
            devicesResponse.EnsureSuccessStatusCode();
            var devicesContent = devicesResponse.Content.ReadAsStringAsync();
            var devices = JsonConvert.DeserializeObject<DevicesResponse>(devicesContent.Result);
            return devices;
        }

        public static async Task<DeviceDetailsResponse> GetDeviceDetails(string slug) {
            var deviceResponse = await _client.GetAsync($"http://localhost:8080/specs/{slug}");
            deviceResponse.EnsureSuccessStatusCode();
            var deviceContent = deviceResponse.Content.ReadAsStringAsync();
            var device = JsonConvert.DeserializeObject<DeviceDetailsResponse>(deviceContent.Result);
            return device;
        }

        public static async Task<string> GetRawDeviceDetails(string slug) {
            var deviceResponse = await _client.GetAsync($"http://localhost:8080/specs/{slug}");
            deviceResponse.EnsureSuccessStatusCode();
            var deviceContent = deviceResponse.Content.ReadAsStringAsync();
            return deviceContent.Result;
        }

        public static void SaveAllDevicesList() {
            var brands = GsmArenaApi.GetAllBrands().Result.Data;
            var devices = new List<DevicesResponse.Device>();
            var brandCount = 1;

            foreach (var brand in brands) {
                System.Console.WriteLine($"{brandCount++}/{brands.Length} ");

                var deviceCount = 0;
                var totalPages = 0;
                var currentPage = 1;
                do {
                    try {
                        var brandDevicesResponse = GsmArenaApi.GetDevicesFromTheBrand(brand.Slug, currentPage).Result;
                        var brandDevices = brandDevicesResponse.Data.Items;
                        totalPages = (int) brandDevicesResponse.Data.TotalPage;
                        System.Console.WriteLine($"{brand.Name} ({currentPage} page out of {totalPages})");
                        currentPage++;
                        foreach (var device in brandDevices) {
                            device.Brand = brand.Name;
                            devices.Add(device);
                            deviceCount++;
                        }
                    } catch (Exception e) {
                        System.Console.WriteLine($"Brand {brand.Name} Failed");
                        System.Console.WriteLine(e.ToString());
                    }

                } while (currentPage <= totalPages);
                System.Console.WriteLine($"Total amount of {brand.Name} devices: {deviceCount}");
            }

            File.WriteAllText("AllDevices.json", JsonConvert.SerializeObject(devices.OrderByDescending(p => p.Id), Formatting.Indented));
        }

        public static void SaveAllDevicesDetails() {
            if (File.Exists("Successful.txt"))
                File.Delete("Successful.txt");
            if (File.Exists("Failed.txt"))
                File.Delete("Failed.txt");

            var devices = JsonConvert
                .DeserializeObject<List<DevicesResponse.Device>>(File.ReadAllText("AllDevices.json"));
            var startTime = DateTime.Now;
            File.AppendAllText($"AllDevicesDetails_{startTime.ToString("yyyy-MM-dd")}.json", "[\n");
            var counter = 0;
            var failed = 0;
            var successfull = 0;

            foreach (var device in devices) {
                System.Console.Write($"{counter.ToString("D4")}/{devices.Count}");
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.Write($"{(failed > 0 ? "-" + failed : "")}");
                Console.ResetColor();
                System.Console.Write($", {((double)counter/devices.Count).ToString("P")}");
                System.Console.WriteLine($", {device.Slug};\n{device.Brand} - {device.Name}");
                counter++;
                try {
                    var deviceDetails = GsmArenaApi.GetRawDeviceDetails(device.Slug).Result;
                    File.AppendAllText($"AllDevicesDetails_{startTime.ToString("yyyy-MM-dd")}.json", (successfull++ > 0 ? "\n," : "") + deviceDetails.Insert(9, $"\"id\":\"{device.Id}\",\"slug\":\"{device.Slug}\",") /*  + ",\n" */ );
                    File.AppendAllText("Successful.txt", $"{device.Slug}; {device.Brand} - {device.Name}\n");
                } catch (System.Exception) {
                    failed++;
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine($"Failed x{failed}!!");
                    Console.ResetColor();
                    File.AppendAllText("Failed.txt", $"{device.Slug}; {device.Brand} - {device.Name}\n");
                }
            }
            File.AppendAllText($"AllDevicesDetails_{DateTime.Now.ToString("yyyy-MM-dd")}.json", "\n]");
            if (File.Exists("AllDevicesDetails.json"))
                File.Delete("AllDevicesDetails.json");
            File.Copy($"AllDevicesDetails_{DateTime.Now.ToString("yyyy-MM-dd")}.json", "AllDevicesDetails.json");
            System.Console.WriteLine($"gud: {successfull} devices");
        }
    }
}
