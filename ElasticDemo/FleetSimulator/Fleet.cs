namespace IotFleet
{
    public class Fleet
    {
        public string Version = "V1.0.0";

        private readonly Dictionary<string, IThingHandler> _handlers = new();

        private readonly IDataProcessor _dataProcessor; //shall be injected by client-app (host)

        //ctor
        public Fleet(IDataProcessor dataProcessor)
        {
            _dataProcessor = dataProcessor;
        }

        public async Task RegisterThing(VehicleData vehicleData, Action<object, RegisterInfo> registeredCallBack, EventHandler<ThingData> eventHandler)
        {

            if (!_handlers.ContainsKey(vehicleData.LicensePlate))
            {

                var thingHandler = new SimulatedThingHandler(); //need to replace with original IOT-Handler

                thingHandler.DataHandler += ThingDataChanged; //local handler subscription

                thingHandler.DataHandler += eventHandler; //external handler example

                _handlers.Add(vehicleData.LicensePlate, thingHandler); //Add to dictionary

                registeredCallBack?.Invoke(this,
                    new RegisterInfo { LicensePlate = vehicleData.LicensePlate }); //callback example

                await thingHandler.Start(vehicleData); //last line
            }
            else
            {
                var vehicleHandler = _handlers[vehicleData.LicensePlate];
                vehicleHandler?.Start(vehicleData);

                registeredCallBack?.Invoke(this,
                    new RegisterInfo { LicensePlate = vehicleData.LicensePlate }); //callback
            }
        }

        private async void ThingDataChanged(object sender, ThingData e)
        {
            await _dataProcessor.Process(e);
        }


        public bool UnRegisterThing(string licensePlate, EventHandler<ThingData> eventHandler)
        {
            if (_handlers.ContainsKey(licensePlate))
            {
                var thingHandler = _handlers[licensePlate];

                thingHandler.DataHandler -= ThingDataChanged; //local handler unsubscribe

                thingHandler.DataHandler -= eventHandler; //needed

                thingHandler.Stop();

                var result = _handlers.Remove(licensePlate);

                return result;
            }

            return true;
        }


        public Task Shutdown(EventHandler<ThingData> eventHandler)
        {
            var vehicles = GetVehicles();
            foreach (var vehicle in vehicles)
            {
                UnRegisterThing(vehicle, eventHandler);
            }

            return Task.FromResult(true);
        }

        //Return Handler
        public IThingHandler GetHandlerByKey(string licensePlate)
        {
            var handler = _handlers[licensePlate];
            return handler;
        }

        //return list of LicensePlates
        public List<string> GetVehicles()
        {
            return _handlers.Keys.ToList();
        }

    }
}
