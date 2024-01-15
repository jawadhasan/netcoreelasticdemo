using System.Diagnostics;

namespace IotFleet
{
    public class RideSimulator
    {
        public event EventHandler<ThingData> NewDataHandler;
        protected VehicleData TripConfigs { get; private set; }
        protected bool _keepRunning { get; private set; } = true;
        
        public async Task Start(VehicleData vehicleData)
        {
            TripConfigs = vehicleData;
            await GenerateRideData(15 * 60); //1*60 = 1 minute
        }
        private async Task GenerateRideData(int driveTime)
        {
            var rnd = new Random();
            var rideId = (int) Math.Floor(rnd.NextDouble() * 1000);
            var counter = 0;
            var endTime = DateTime.UtcNow;

            var sw = new Stopwatch();
            sw.Start();

            while (_keepRunning)//sw.ElapsedMilliseconds < driveTime * 1000
            {
                counter++;
                Console.WriteLine($"Key: {TripConfigs.LicensePlate}: Counter: {counter}");
                //slightly different from NodeJS example
                var newLat = TripConfigs.StartCoordinates.Lat + (TripConfigs.EndCoordinates.Lat - TripConfigs.StartCoordinates.Lat) * (driveTime / counter);
                var newLon = TripConfigs.StartCoordinates.Lon + (TripConfigs.EndCoordinates.Lon - TripConfigs.StartCoordinates.Lon) * (driveTime / counter);

                var currentCoordinates = new Coordinates
                {
                    Lat = newLat,
                    Lon = newLon
                };

                var nd = endTime.Ticks - (driveTime - counter) * 60 * 1000;

                var ts = new DateTime(nd).Ticks;

                var data = new ThingData()
                {
                    LicensePlate = TripConfigs.LicensePlate,
                    RideId = rideId,
                    Temperature = TripConfigs.Temperature + 0.02 * counter,
                    Ts = ts,
                    Lat = currentCoordinates.Lat,
                    Lon = currentCoordinates.Lon
                };
                NewDataHandler?.Invoke(this, data); //broadcast event
                var randomDelay = rnd.Next(3000, 5000);

                await Task.Delay(randomDelay);
            }
        }

        public async Task Stop()
        {
            _keepRunning = false;
            await Task.FromResult(true);

        }

    }

}
