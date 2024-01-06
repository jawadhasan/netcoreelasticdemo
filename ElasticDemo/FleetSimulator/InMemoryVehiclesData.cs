namespace IotFleet
{

    //Helper class to generate data for testing purposes
    public static class InMemoryVehiclesData
    {
        public static List<VehicleData> GetTripConfigs()
        {
            var list = new List<VehicleData>();

            var vehicleData1 = new VehicleData
            {
                LicensePlate = "AAA000",
                StartCoordinates = new Coordinates
                {
                    Lat = 46.6314609,
                    Lon = -99.3446777
                },
                EndCoordinates = new Coordinates
                {
                    Lat = 46.6302106,
                    Lon = -96.8319174
                },
                Temperature = 60.3
            };


            var vehicleData2 = new VehicleData
            {
                LicensePlate = "BBB000",
                StartCoordinates = new Coordinates
                {
                    Lat = 45.6314609,
                    Lon = -97.3446777
                },
                EndCoordinates = new Coordinates
                {
                    Lat = 45.6302106,
                    Lon = -94.8319174
                },
                Temperature = 52.5
            };


            var vehicleData3 = new VehicleData
            {
                LicensePlate = "CCC000",
                StartCoordinates = new Coordinates
                {
                    Lat = 44.6314609,
                    Lon = -95.3446777
                },
                EndCoordinates = new Coordinates
                {
                    Lat = 44.6302106,
                    Lon = -92.8319174
                },
                Temperature = 67.8
            };

            list.Add(vehicleData1);
            list.Add(vehicleData2);
            list.Add(vehicleData3);

            return list;
        }
    }
}
