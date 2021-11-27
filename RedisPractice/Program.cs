using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace RedisPractice
{
    class Program
    {
        static void Main()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appSettings.json"))
                .Build();

            var host = config["host"];
            var port = config["port"];

            Console.WriteLine($"Host: {host}");
            Console.WriteLine($"Port: {port}");

            var redisClient = new RedisClient(host, port);

            Console.WriteLine($"Set dt variable with current date");

            redisClient
                .Set("dt", DateTime.Now.ToString("r"));

            Console.ReadLine();
        }
    }
}
