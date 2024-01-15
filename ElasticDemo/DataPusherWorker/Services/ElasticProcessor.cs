using System;
using System.Threading.Tasks;
using IotFleet;
using Microsoft.Extensions.Logging;
using Nest;

namespace DataPusherWorker.Services
{
    //Injected into Fleet: also need to call AddElasticsearch first as setup
    public class ElasticProcessor : IDataProcessor
    {
        private readonly ILogger<ElasticProcessor> _logger;
        private readonly IElasticClient _elasticClient;

        public ElasticProcessor(ILogger<ElasticProcessor> logger, IElasticClient elasticClient)
        {
            _logger = logger;
            _elasticClient = elasticClient;
        }
        public async Task Process(RideData rideData)
        {
            _logger.LogInformation($"{DateTime.Now} {rideData.LicensePlate} {rideData.Temperature} {rideData.Lat} {rideData.Lon}");
           
            //...TODO use elasticclient

           await _elasticClient.IndexDocumentAsync(rideData);
        }

        public Task Setup()
        {
            _logger.LogInformation($"ESProcessor Setup() called!");
            return Task.CompletedTask;
        }
    }
}
