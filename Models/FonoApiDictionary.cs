namespace QuickType
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    public class FonoApiDictionary
    {
        public Dictionary<string, string> Details { get; set; }
        //  public static Dictionary<string, string>[] FromJson(string json) => JsonConvert.DeserializeObject<Dictionary<string, string>[]>(json, QuickType.Converter.Settings);
    }

    // public static class Serialize
    // {
    //     public static string ToJson(this Dictionary<string, string>[] self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
    // }

    // internal static class Converter
    // {
    //     public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    //     {
    //         MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
    //         DateParseHandling = DateParseHandling.None,
    //         Converters = {
    //             new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
    //         },
    //     };
    // }
}