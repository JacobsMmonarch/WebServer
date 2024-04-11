using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

class Prog
{
    public static async Task Main(string[] args)
    {
        string logFilePath = "server.log";

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        ServerLogger logger = new ServerLogger(logFilePath, configuration);

        await logger.LogResponseTimeAsync();
    }
}