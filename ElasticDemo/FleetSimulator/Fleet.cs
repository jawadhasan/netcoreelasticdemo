namespace IotFleet
{
    public class Fleet
    {
        public string Version = "V1.0.0";

        private readonly Dictionary<string, IThingHandler> _handlers = new();

        private readonly IPersistenceService _persistenceSRV; //shall be injected by client-app (host)

        //ctor
        public Fleet(IPersistenceService persistenceSRV)
        {
            _persistenceSRV = persistenceSRV;
        }

        public async Task RegisterVehicle(VehicleData vehicleData, Action<object, RegisterInfo> registeredCallBack, EventHandler<RideData> eventHandler)
        {

            if (!_handlers.ContainsKey(vehicleData.LicensePlate))
            {

                var thingHandler = new SimulatedVehicleHandler(); //need to replace with original IOT-Handler

                thingHandler.DataHandler += VehicleThingRideDataChanged; //local handler subscription

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

        private async void VehicleThingRideDataChanged(object sender, RideData e)
        {
            await _persistenceSRV.Save(e); // _elasticClientService
        }


        public bool UnRegisterVehicle(string licensePlate, EventHandler<RideData> eventHandler)
        {
            if (_handlers.ContainsKey(licensePlate))
            {
                var thingHandler = _handlers[licensePlate];

                thingHandler.DataHandler -= VehicleThingRideDataChanged; //local handler unsubscribe

                thingHandler.DataHandler -= eventHandler; //needed

                thingHandler.Stop();

               var result = _handlers.Remove(licensePlate);

                return result;
            }

            return true;
        }


        public Task Shutdown(EventHandler<RideData> eventHandler)
        {
            var vehicles = GetVehicles();
            foreach (var vehicle in vehicles)
            {
                UnRegisterVehicle(vehicle, eventHandler);
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
