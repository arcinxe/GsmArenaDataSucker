using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GsmArenaDataSucker.Models
{
    public class DevicesResponse
    {
        [JsonProperty("data")]
        public Datum Data { get; set; }

        [JsonProperty("error")]
        public object Error { get; set; }

        public partial class Datum
        {
            [JsonProperty("items")]
            public List<Device> Items { get; set; }

            [JsonProperty("current_page")]
            public long CurrentPage { get; set; }

            [JsonProperty("total_page")]
            public long TotalPage { get; set; }
        }

        public partial class Device
        {
            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("brand")]
            public string Brand { get; set; }

            [JsonProperty("slug")]
            public string Slug { get; set; }

            [JsonProperty("image")]
            public Uri Image { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }
        }

    }
}