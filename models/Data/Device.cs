using PublicTransportDevices.Models.Data.Enums; 

namespace PublicTransportDevices.Models.Data;

public class Device
{
    public string Uid { get; set; } = ""; 
    public DeviceType DeviceType { get; set; }
}
