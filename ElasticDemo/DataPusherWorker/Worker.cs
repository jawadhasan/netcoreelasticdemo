using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nest;

namespace DataPusherWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ElasticClient _elasticClient;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;

            //1. Setup Connection and ElasticClient
            var node = new Uri("http://localhost:9200");

            var setting = new ConnectionSettings(node)
                .DefaultIndex("demodevicedata");

            _elasticClient = new ElasticClient(setting);
        }


        public void test()
        {

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var deviceData = MockDeviceDataService.GetMockData();
                var indexResponse = _elasticClient.IndexDocument(deviceData);
               
                //Console.WriteLine(indexResponse.DebugInformation);


                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
