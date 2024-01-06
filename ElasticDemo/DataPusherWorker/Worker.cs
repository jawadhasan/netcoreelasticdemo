using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IotFleet;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DataPusherWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
       
        private readonly Fleet _fleet;

        private readonly WebsocketClient _webSocketClient;

        public Worker(ILogger<Worker> logger, Fleet fleet, WebsocketClient websocketClient)
        {
            _logger = logger;

            _fleet = fleet;

            _webSocketClient = websocketClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            await _webSocketClient.Connect();

            var tasksList = new List<Task>();
            
            var vehiclesList = InMemoryVehiclesData.GetTripConfigs(); //TODO: e.g. can load from DB

            foreach (var vehicle in vehiclesList)
            {
                var task = Task.Factory.StartNew(async() => await _fleet.RegisterVehicle(vehicle, registeredCallbackHandler, HandleEvent), stoppingToken);
                tasksList.Add(task);
            }

            _logger.LogWarning($"VehicleCount: {_fleet.GetVehicles().Count}");
        }


        //EventHandling and Callback
        private async void HandleEvent(object sender, RideData e)
        {
            _logger.LogDebug($"worker event-Handler {e.LicensePlate}");
           await _webSocketClient.Save(e);
        }
        private void registeredCallbackHandler(object sender, RegisterInfo e)
        {
            _logger.LogWarning($"worker callback executed! license: {e.LicensePlate}");
        }


        private void Testingcode()
        {
            //yield-return version
            //foreach (var rideData in _rideSimulator.GenerateRideData("ABC000"))
            //{
            //    //received via yeild return==> 

            //    _logger.LogInformation($"TS: {rideData.Ts}, LicensePlate: {rideData.LicensePlate}, RideId: {rideData.RideId}, LatLong:[{rideData.Lat} {rideData.Lon}], Temp:{rideData.Temperature}");

            //    //await  _elasticClient.IndexDocumentAsync(rideData, stoppingToken);

            //}




            ////prepare json
            //var jsonData = JsonConvert.SerializeObject(data, new JsonSerializerSettings
            //{
            //    ContractResolver = new CamelCasePropertyNamesContractResolver()
            //});

            ////publish to AWS IoT Core
            //mqttClient.Publish("truck_sensor", Encoding.UTF8.GetBytes(jsonData));

            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    var deviceData = MockDeviceDataService.GetMockData();
            //    var indexResponse = _elasticClient.IndexDocument(deviceData);

            //    await Task.Delay(5000, stoppingToken);
            //}
        }
    }
}
