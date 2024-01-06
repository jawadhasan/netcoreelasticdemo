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
                   
                    //services.AddTransient<IPersistenceService, ConsolePersistence>(); //a sample persistence from simulator-library
                    services.AddTransient<IPersistenceService, ElasticPersistenceService>(); //persistence service from application; can comment above line.

                  
                    services.AddSingleton<Fleet>();

                    //services.AddSingleton<FleetSimulator>(_ => new FleetSimulator(args[0],args[1])); //to run separate ride-instanance approach

                    services.AddElasticsearch(hostContext.Configuration);//see extension method

                    services.AddHostedService<Worker>();
                });
    }
}
