using System;
using DataPusherWorker.Services;
using IotFleet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace DataPusherWorker
{
    public static class ServiceCollectionExtensions
    {
        public static void AddElasticsearch(
            this IServiceCollection services, IConfiguration configuration)
        {
            try
            {
                var url = configuration["elasticsearch:url"];
                var defaultIndex = configuration["elasticsearch:index"];

                var settings = new ConnectionSettings(new Uri(url))
                    .DefaultIndex(defaultIndex);
                    //.DefaultMappingFor<DeviceData>(m => m);

                settings.DefaultFieldNameInferrer(p => p);

                var client = new ElasticClient(settings);

                services.AddSingleton<IElasticClient>(client);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }


        public static void AddDataProcessor(
            this IServiceCollection services, IConfiguration configuration)
        {
            try
            {
                var url = configuration["socketserver:url"];

                //WebSocket registration
                //var client = new WebSocketProcessor(url);
                //services.AddSingleton<DataProcessor>(client);  //services.AddSingleton(client);

                //Console Registration
                //services.AddSingleton<IDataProcessor, ConsoleDataProcessor>();
                //services.AddSingleton<IDataProcessor, ElasticProcessor>();

            }
            catch (Exception ex)
            {
                var error = ex.Message;
                Console.WriteLine(ex.Message);
            }
        }
    }
}
