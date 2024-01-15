namespace IotFleet;


//client app need to inject implementation
public interface IDataProcessor
{
    Task Process(RideData rideData);
    Task Setup();
}

//sample persistence
public class ConsoleDataProcessor : IDataProcessor
{
    public async Task Process(RideData rideData)
    {

        Console.WriteLine($"RideData received in fleet {rideData.LicensePlate}: {rideData.Temperature}");
        await Task.FromResult(0);
    }

    public async Task Setup()
    {
        Console.WriteLine($"ConsolePersistence setup() called");
    }
}




public class RegisterInfo
{
    public string LicensePlate { get; set; }
}


public class VehicleData
{
    public string LicensePlate { get; set; }
    public Coordinates StartCoordinates { get; set; }
    public Coordinates EndCoordinates { get; set; }
    public double Temperature { get; set; }
}
public class RideData
{
    public string LicensePlate { get; set; } // PK of Master Entity
    public int RideId { get; set; }
    public double Temperature { get; set; }
    public long Ts { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }
}
public class Coordinates
{
    public double Lat { get; set; }
    public double Lon { get; set; }
}