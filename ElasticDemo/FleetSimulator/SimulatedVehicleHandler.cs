namespace IotFleet
{
    public interface IThingHandler
    {
        public event EventHandler<RideData> DataHandler;
        Task Start(VehicleData vehicleData);
        Task Stop();
    }

    public class SimulatedVehicleHandler : IThingHandler
    {
        private readonly RideSimulator _thing;

        public event EventHandler<RideData> DataHandler;
        public VehicleData VehicleData { get; private set; }

        public List<RideData> History { get; private set; } = new();

        public SimulatedVehicleHandler()
        {
            
            _thing = new RideSimulator();
            _thing.NewDataHandler += DataChangedHandler;
        }



        //the bridge
        private void DataChangedHandler(object sender, RideData e)
        {
            History.Add(e); //store to local history

            DataHandler?.Invoke(this, e); //broadcast to subscribers
        }

        public async Task Start(VehicleData vehicleData)
        {
            VehicleData = vehicleData;
            await _thing.Start(VehicleData);
        }

        public async Task Stop()
        {
            await _thing.Stop();
        }

    }
}
