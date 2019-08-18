using Newtonsoft.Json;

namespace GsmArenaDataSucker.Models
{
    public class BrandsResponse
    {
        [JsonProperty("data")]
        public Datum[] Data { get; set; }

        [JsonProperty("error")]
        public object Error { get; set; }
        public class Datum
        {
            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("slug")]
            public string Slug { get; set; }

            [JsonProperty("number_of_devices")]
            public long NumberOfDevices { get; set; }
        }
    }
}