using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DataPusherWorker.Services;
using IotFleet;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DataPusherWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly Fleet _fleet;

        private readonly IDataProcessor _dataProcessor;
        private readonly IEventConsumer _eventConsumer;

        private readonly Random _random = new Random();

        public Worker(ILogger<Worker> logger, Fleet fleet, IDataProcessor dataProcessor, IEventConsumer eventConsumer)
        {
            _logger = logger;

            _fleet = fleet;

            _dataProcessor = dataProcessor;

            _eventConsumer = eventConsumer;
        }


        //this method is called, when an external message is recvd e.g. SQS message
        public void eventCallbackHandler(object sender, ExternalEvent eventPayload)
        {
            var randomTemp = _random.NextInt64(50, 60) + _random.NextDouble();

            //register
            if (eventPayload.EventType == EventType.VehicleSaved || eventPayload.EventType == EventType.StartVehicle)
            {
                //register : TODO: random coordinates start/end
                var vehicle = new VehicleData();
                vehicle.LicensePlate = eventPayload.LicensePlate;
                vehicle.Temperature = 67.5;
                vehicle.StartCoordinates = new Coordinates
                {
                    Lat = 46.6314609,
                    Lon = -99.3446777
                };
                vehicle.EndCoordinates = new Coordinates
                {
                    Lat = 46.6302106,
                    Lon = -96.8319174
                };
                vehicle.Temperature = randomTemp;

                var task = Task.Factory.StartNew(async () => await _fleet.RegisterThing(vehicle, registeredCallbackHandler, HandleEvent), CancellationToken.None);

            }
            else if (eventPayload.EventType == EventType.VehicleDeleted || eventPayload.EventType == EventType.StopVehicle)
            {
                var result = _fleet.UnRegisterThing(eventPayload.LicensePlate, HandleEvent);
                _logger.LogWarning($"Un-registered vehicle {eventPayload.LicensePlate} success {result}");
            }
            else if (eventPayload.EventType == EventType.Shutdown)
            {
                var result = _fleet.Shutdown(HandleEvent);
            }
            else
            {
                _logger.LogWarning($"TODO: eventType {eventPayload.EventType} received");
            }

            _logger.LogWarning($"eventType {eventPayload.EventType} processed for {eventPayload.LicensePlate}");
        }



        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            await _dataProcessor.Setup();

            await InitializeFleet(stoppingToken); //initialize with test data



            if (stoppingToken.IsCancellationRequested)
            {
                //shutdown fleet
                await _fleet.Shutdown(HandleEvent);

                //stop event consumer
                _eventConsumer.Stop();
            }

            _logger.LogWarning($"VehicleCount: {_fleet.GetVehicles().Count}");
        }


        private async Task InitializeFleet(CancellationToken stoppingToken)
        {
            #region testCode
            //var tasksList = new List<Task>();

            //var vehiclesList = InMemoryVehiclesData.GetTripConfigs(); //TODO: e.g. load from DB or from a REST endpoint etc.
            //foreach (var vehicle in vehiclesList)
            //{
            //    var task = Task.Factory.StartNew(async () => await _fleet.RegisterThing(vehicle, registeredCallbackHandler, HandleEvent), stoppingToken);
            //    tasksList.Add(task);
            //}
            #endregion

            // Starting event consumer
            _eventConsumer.Start(eventCallbackHandler);
        }


        //EventHandling and Callback
        private async void HandleEvent(object sender, ThingData e)
        {
            //TODO: This is an hook-point, if app want to process the data (currently no need as we are injecting dataProcessor to fleet)
            //await _dataProcessor.Process(e);

            _logger.LogDebug($"NewData event-Handler {e.LicensePlate}");
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
