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
            System.Console.WriteLine("STUFF STARTED!");
            GsmArenaApi.SaveAllPhonesList();
            GsmArenaApi.SaveAllPhonesDetails();
            // FonoApi.SaveAllPhoneDetails();
        }
    }
}
