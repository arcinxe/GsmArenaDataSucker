using System;
using Newtonsoft;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using GsmArenaDataSucker.Models;
using System.Linq;
using GsmArenaDataSucker;
using System.Collections.Generic;

namespace GsmArenaDataSucker
{
    class Program
    {
        static void Main(string[] args)
        {
            var startTime = DateTime.Now;
            System.Console.WriteLine($"Started at {startTime}");
            GsmArenaApi.SaveAllDevicesList();
            GsmArenaApi.SaveAllDevicesDetails();
            FonoApi.SaveAllDevicesDetails();
            System.Console.WriteLine($"Done in: ~{(DateTime.Now - startTime):mm\\m\\:ss\\s\\:fff\\m\\s}!");
        }
    }
}
