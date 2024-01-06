using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
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


        public static void AddWebsocketclient(
            this IServiceCollection services, IConfiguration configuration)
        {
            try
            {
                var url = configuration["socketserver:url"];

                var client = new WebsocketClient(url);

                services.AddSingleton(client);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }
        }
    }
}
