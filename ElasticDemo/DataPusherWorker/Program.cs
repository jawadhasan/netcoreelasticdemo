using DataPusherWorker.Services;
using DataPusherWorker.Settings;
using IotFleet;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DataPusherWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<SQSSettings>(hostContext.Configuration.GetSection("Queueing"));
                    services.AddSingleton<IEventConsumer, SQSEventConsumer>();

                    //services.AddTransient<IPersistenceService, ConsolePersistence>(); //a sample persistence from simulator-library
                    services.AddSingleton<IPersistenceService, ElasticPersistenceService>(); //persistence service from application; can comment above line.
                    services.AddDataProcessor(hostContext.Configuration);//e.g. websocketProcessor, consoleProcessor etc.
                    services.AddElasticsearch(hostContext.Configuration);//see extension method

                    services.AddSingleton<Fleet>();  //services.AddSingleton<FleetSimulator>(_ => new FleetSimulator(args[0],args[1])); //to run separate ride-instanance approach

                    services.AddHostedService<Worker>();
                });
    }
}
