using Newtonsoft.Json;

namespace ConsoleApp.Models
{
        public class FonoApiDetails
    {

        [JsonProperty("DeviceName")]
        public string DeviceName { get; set; }

        [JsonProperty("Brand")]
        public string Brand { get; set; }

        [JsonProperty("technology")]
        public string Technology { get; set; }

        [JsonProperty("gprs")]
        public string Gprs { get; set; }

        [JsonProperty("edge")]
        public string Edge { get; set; }

        [JsonProperty("announced")]
        public string Announced { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("dimensions")]
        public string Dimensions { get; set; }

        [JsonProperty("weight")]
        public string Weight { get; set; }

        [JsonProperty("sim")]
        public string Sim { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("resolution")]
        public string Resolution { get; set; }

        [JsonProperty("card_slot")]
        public string CardSlot { get; set; }

        [JsonProperty("alert_types")]
        public string AlertTypes { get; set; }

        [JsonProperty("loudspeaker_")]
        public string Loudspeaker { get; set; }

        [JsonProperty("wlan")]
        public string Wlan { get; set; }

        [JsonProperty("bluetooth")]
        public string Bluetooth { get; set; }

        [JsonProperty("gps")]
        public string Gps { get; set; }

        [JsonProperty("radio")]
        public string Radio { get; set; }

        [JsonProperty("usb")]
        public string Usb { get; set; }

        [JsonProperty("messaging")]
        public string Messaging { get; set; }

        [JsonProperty("browser")]
        public string Browser { get; set; }

        [JsonProperty("java")]
        public string Java { get; set; }

        [JsonProperty("features_c")]
        public string FeaturesC { get; set; }

        [JsonProperty("battery_c")]
        public string BatteryC { get; set; }

        [JsonProperty("stand_by")]
        public string StandBy { get; set; }

        [JsonProperty("talk_time")]
        public string TalkTime { get; set; }

        [JsonProperty("colors")]
        public string Colors { get; set; }

        [JsonProperty("sensors")]
        public string Sensors { get; set; }

        [JsonProperty("cpu")]
        public string Cpu { get; set; }

        [JsonProperty("internal")]
        public string Internal { get; set; }

        [JsonProperty("os")]
        public string Os { get; set; }

        [JsonProperty("primary_")]
        public string Primary { get; set; }

        [JsonProperty("video")]
        public string Video { get; set; }

        [JsonProperty("secondary")]
        public string Secondary { get; set; }

        [JsonProperty("speed")]
        public string Speed { get; set; }

        [JsonProperty("chipset")]
        public string Chipset { get; set; }

        [JsonProperty("features")]
        public string Features { get; set; }

        [JsonProperty("gpu")]
        public string Gpu { get; set; }

        [JsonProperty("multitouch")]
        public string Multitouch { get; set; }

        [JsonProperty("_2g_bands")]
        public string Bands2g { get; set; }

        [JsonProperty("_3_5mm_jack_")]
        public string Jack35mm { get; set; }

        [JsonProperty("_3g_bands")]
        public string Bands3g { get; set; }

        [JsonProperty("_4g_bands")]
        public string Bands4g { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }
    }


}