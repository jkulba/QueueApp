using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Serilog;
using Serilog.Events;

namespace Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //await CreateHostBuilder(args).Build().RunAsync();
            IHost host = CreateHostBuilder(args).Build();
            IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

            string connectionString = config.GetValue<string>("StorageConnectionString");
            string requestInputQueue = config.GetValue<string>("RequestInputQueue");

            Console.WriteLine($"connectionString = {connectionString}");
            Console.WriteLine($"requestInputQueue = {requestInputQueue}");

            // // Get values from the config given their key and their target type.
            // int keyOneValue = config.GetValue<int>("KeyOne");
            // bool keyTwoValue = config.GetValue<bool>("KeyTwo");
            // string keyThreeNestedValue = config.GetValue<string>("KeyThree:Message");

            // // Write the values to the console.
            // Console.WriteLine($"KeyOne = {keyOneValue}");
            // Console.WriteLine($"KeyTwo = {keyTwoValue}");
            // Console.WriteLine($"KeyThree:Message = {keyThreeNestedValue}");

            await host.RunAsync();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((host, log) =>
                {
                    if (host.HostingEnvironment.IsProduction())
                        log.MinimumLevel.Information();
                    else
                        log.MinimumLevel.Debug();

                    log.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
                    log.WriteTo.Console();
                })
                .ConfigureServices((hostContext, services) =>
                {

                });
    }
}


