using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp1.Tests
{
    public static class Configuration
    {
        static Configuration() {
            var builder = new ConfigurationBuilder().AddJsonFile("TestingParam.json");
            Get = builder.Build();
        }
        public static IConfiguration Get { get; set; }
    }
}
