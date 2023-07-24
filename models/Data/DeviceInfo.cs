namespace PublicTransportDevices.Models.Data;

public class DeviceInfo
{
    public string Uid { get; set; } = ""; 
    public GeoCoordinate GeoCoordinate { get; set; }
    public System.DateTime DateTimeCreated { get; set; }
}
