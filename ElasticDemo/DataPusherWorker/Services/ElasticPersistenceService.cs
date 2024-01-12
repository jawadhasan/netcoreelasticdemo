using System;
using System.Threading.Tasks;
using IotFleet;
using Microsoft.Extensions.Logging;
using Nest;

namespace DataPusherWorker.Services
{
    //Injected into Fleet
    public class ElasticPersistenceService : IPersistenceService
    {
        private readonly ILogger<ElasticPersistenceService> _logger;
        private readonly IElasticClient _elasticClient;

        public ElasticPersistenceService(ILogger<ElasticPersistenceService> logger, IElasticClient elasticClient)
        {
            _logger = logger;
            _elasticClient = elasticClient;
        }
        public async Task Save(RideData rideData)
        {
            _logger.LogInformation($"{DateTime.Now} {rideData.LicensePlate} {rideData.Temperature} {rideData.Lat} {rideData.Lon}");
           
            //...TODO use elasticclient

           await _elasticClient.IndexDocumentAsync(rideData);
        }
    }
}
