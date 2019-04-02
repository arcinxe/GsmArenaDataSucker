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
            var startTime = DateTime.Now;
            System.Console.WriteLine($"Started at {startTime}");
            GsmArenaApi.SaveAllPhonesList();
            GsmArenaApi.SaveAllPhonesDetails();
            // FonoApi.SaveAllPhoneDetails();
            System.Console.WriteLine($"Done in: ~{(DateTime.Now - startTime):mm\\m\\:ss\\s\\:fff\\m\\s}!");
        }
    }
}
