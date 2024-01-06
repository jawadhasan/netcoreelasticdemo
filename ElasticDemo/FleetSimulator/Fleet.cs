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
            var thingHandler = new SimulatedVehicleHandler(); //need to replace with original IOT-Handler

            thingHandler.DataHandler += VehicleThingRideDataChanged; //local handler subscription

            thingHandler.DataHandler += eventHandler; //external handler example

            _handlers.Add(vehicleData.LicensePlate, thingHandler); //Add to dictionary

            registeredCallBack?.Invoke(this, new RegisterInfo { LicensePlate = vehicleData.LicensePlate }); //callback example

            await thingHandler.Start(vehicleData); //last line
        }



        private async void VehicleThingRideDataChanged(object sender, RideData e)
        {
           await _persistenceSRV.Save(e); // _elasticClientService
        }

        public IThingHandler GetHandlerByKey(string licensePlate)
        {
            var handler = _handlers[licensePlate];
            return handler;
        }

        public List<string> GetVehicles()
        {
            return _handlers.Keys.ToList();
        }

    }
}
