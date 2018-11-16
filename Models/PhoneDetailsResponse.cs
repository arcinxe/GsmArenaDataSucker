using System;
using Newtonsoft.Json;

namespace ConsoleApp.Models
{
    public class PhoneDetailsResponse
    {
        [JsonProperty("data")]
        public Datum Data { get; set; }

        [JsonProperty("error")]
        public object Error { get; set; }

        public partial class Datum
    {
        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("device_name")]
        public string DeviceName { get; set; }

        [JsonProperty("device_type")]
        public string DeviceType { get; set; }

        [JsonProperty("image_url")]
        public Uri ImageUrl { get; set; }

        [JsonProperty("overview")]
        public Overview Overview { get; set; }

        [JsonProperty("detail")]
        public Detail Detail { get; set; }
    }

    public partial class Detail
    {
        [JsonProperty("battery")]
        public DetailBattery Battery { get; set; }

        [JsonProperty("body")]
        public Body Body { get; set; }

        [JsonProperty("comms")]
        public Comms Comms { get; set; }

        [JsonProperty("display")]
        public DetailDisplay Display { get; set; }

        [JsonProperty("features")]
        public Features Features { get; set; }

        [JsonProperty("launch")]
        public Launch Launch { get; set; }

        [JsonProperty("main__camera")]
        public Camera MainCamera { get; set; }

        [JsonProperty("memory")]
        public Memory Memory { get; set; }

        [JsonProperty("misc")]
        public Misc Misc { get; set; }

        [JsonProperty("network")]
        public Network Network { get; set; }

        [JsonProperty("platform")]
        public Platform Platform { get; set; }

        [JsonProperty("selfie_camera")]
        public Camera SelfieCamera { get; set; }

        [JsonProperty("sound")]
        public Sound Sound { get; set; }

        [JsonProperty("tests")]
        public Tests Tests { get; set; }
    }

    public partial class DetailBattery
    {
        [JsonProperty("")]
        public string Empty { get; set; }

        [JsonProperty("music_play")]
        public string MusicPlay { get; set; }

        [JsonProperty("talk_time")]
        public string TalkTime { get; set; }
    }

    public partial class Body
    {
        [JsonProperty("")]
        public string[] Empty { get; set; }

        [JsonProperty("build")]
        public string Build { get; set; }

        [JsonProperty("dimensions")]
        public string Dimensions { get; set; }

        [JsonProperty("sim")]
        public string Sim { get; set; }

        [JsonProperty("weight")]
        public string Weight { get; set; }
    }

    public partial class Comms
    {
        [JsonProperty("bluetooth")]
        public string Bluetooth { get; set; }

        [JsonProperty("gps")]
        public string Gps { get; set; }

        [JsonProperty("nfc")]
        public string Nfc { get; set; }

        [JsonProperty("radio")]
        public string Radio { get; set; }

        [JsonProperty("usb")]
        public string Usb { get; set; }

        [JsonProperty("wlan")]
        public string Wlan { get; set; }
    }

    public partial class DetailDisplay
    {
        [JsonProperty("")]
        public string[] Empty { get; set; }

        [JsonProperty("multitouch")]
        public string Multitouch { get; set; }

        [JsonProperty("protection")]
        public string Protection { get; set; }

        [JsonProperty("resolution")]
        public string Resolution { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public partial class Features
    {
        [JsonProperty("")]
        public string[] Empty { get; set; }

        [JsonProperty("browser")]
        public string Browser { get; set; }

        [JsonProperty("messaging")]
        public string Messaging { get; set; }

        [JsonProperty("sensors")]
        public string Sensors { get; set; }
    }

    public partial class Launch
    {
        [JsonProperty("announced")]
        public string Announced { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public partial class Camera
    {
        [JsonProperty("features")]
        public string Features { get; set; }

        [JsonProperty("single")]
        public string Single { get; set; }

        [JsonProperty("video")]
        public string Video { get; set; }
    }

    public partial class Memory
    {
        [JsonProperty("card_slot")]
        public string CardSlot { get; set; }

        [JsonProperty("internal")]
        public string Internal { get; set; }
    }

    public partial class Misc
    {
        [JsonProperty("colors")]
        public string Colors { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("sar")]
        public string[] Sar { get; set; }

        [JsonProperty("sar_eu")]
        public string[] SarEu { get; set; }
    }

    public partial class Network
    {
        [JsonProperty("2g_bands")]
        public string The2GBands { get; set; }

        [JsonProperty("3g_bands")]
        public string The3GBands { get; set; }

        [JsonProperty("4g_bands")]
        public string The4GBands { get; set; }

        [JsonProperty("edge")]
        public string Edge { get; set; }

        [JsonProperty("gprs")]
        public string Gprs { get; set; }

        [JsonProperty("speed")]
        public string Speed { get; set; }

        [JsonProperty("technology")]
        public string Technology { get; set; }
    }

    public partial class Platform
    {
        [JsonProperty("chipset")]
        public string Chipset { get; set; }

        [JsonProperty("cpu")]
        public string Cpu { get; set; }

        [JsonProperty("gpu")]
        public string Gpu { get; set; }

        [JsonProperty("os")]
        public string Os { get; set; }
    }

    public partial class Sound
    {
        [JsonProperty("")]
        public string[] Empty { get; set; }

        [JsonProperty("3_5mm_jack")]
        public string The3_5MmJack { get; set; }

        [JsonProperty("alert_types")]
        public string AlertTypes { get; set; }

        [JsonProperty("loudspeaker")]
        public string Loudspeaker { get; set; }
    }

    public partial class Tests
    {
        [JsonProperty("audio_quality")]
        public string AudioQuality { get; set; }

        [JsonProperty("battery_life")]
        public string BatteryLife { get; set; }

        [JsonProperty("camera")]
        public string Camera { get; set; }

        [JsonProperty("display")]
        public string Display { get; set; }

        [JsonProperty("loudspeaker")]
        public string Loudspeaker { get; set; }

        [JsonProperty("performance")]
        public string Performance { get; set; }
    }

    public partial class Overview
    {
        [JsonProperty("general_info")]
        public GeneralInfo GeneralInfo { get; set; }

        [JsonProperty("display")]
        public OverviewDisplay Display { get; set; }

        [JsonProperty("camera")]
        public CameraClass Camera { get; set; }

        [JsonProperty("expansion")]
        public Expansion Expansion { get; set; }

        [JsonProperty("battery")]
        public OverviewBattery Battery { get; set; }
    }

    public partial class OverviewBattery
    {
        [JsonProperty("capacity")]
        public string Capacity { get; set; }

        [JsonProperty("technology")]
        public string Technology { get; set; }
    }

    public partial class CameraClass
    {
        [JsonProperty("photo")]
        public string Photo { get; set; }

        [JsonProperty("video")]
        public string Video { get; set; }
    }

    public partial class OverviewDisplay
    {
        [JsonProperty("touchscreen")]
        public string Touchscreen { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("resolution")]
        public string Resolution { get; set; }
    }

    public partial class Expansion
    {
        [JsonProperty("ram")]
        public string Ram { get; set; }

        [JsonProperty("chipset")]
        public string Chipset { get; set; }
    }

    public partial class GeneralInfo
    {
        [JsonProperty("launched")]
        public string Launched { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("os")]
        public string Os { get; set; }

        [JsonProperty("storage")]
        public string Storage { get; set; }
    }
    }
}