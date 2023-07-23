namespace PublicTransportDevices.Models;

public enum DeviceType
{
    VehicleValidator = 1, 
    DriverConsole = 2, 
    DriverDisplay = 3, 
    OnboardPassengerInformation = 4, 
    BoardComputer = 5, 
    PassengerCounting = 6, 
    VideoCamera = 7
}

public class GeoCoordinate
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

public class Device
{
    public string Uid { get; set; } = ""; 
    public DeviceType DeviceType { get; set; }
}

public class DeviceInfo
{
    public string Uid { get; set; } = ""; 
    public GeoCoordinate GeoCoordinate { get; set; }
}
