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
        public static async Task<BrandsResponse> GetAllBrands(){
            var brandsResponse = await _client.GetAsync("http://localhost:8080/brands");
            brandsResponse.EnsureSuccessStatusCode();
            var brandsContent = brandsResponse.Content.ReadAsStringAsync();
            var brands = JsonConvert.DeserializeObject<BrandsResponse>(brandsContent.Result);
            return brands;
        }

        public static async Task<PhonesResponse> GetPhonesFromTheBrand(string slug, int page = 1){
            var phonesResponse = await _client.GetAsync($"http://localhost:8080/devices/{slug}?page={page}");
            phonesResponse.EnsureSuccessStatusCode();
            var phonesContent = phonesResponse.Content.ReadAsStringAsync();
            var phones = JsonConvert.DeserializeObject<PhonesResponse>(phonesContent.Result);
            return phones;
        }

         public static async Task<PhoneDetailsResponse> GetPhoneDetails(string slug){
            var phoneResponse = await _client.GetAsync($"http://localhost:8080/specs/{slug}");
            phoneResponse.EnsureSuccessStatusCode();
            var phoneContent = phoneResponse.Content.ReadAsStringAsync();
            File.WriteAllText("rawPhoneDetails.json", phoneContent.Result);
            var phone = JsonConvert.DeserializeObject<PhoneDetailsResponse>(phoneContent.Result);
            return phone;
        }
    }
}