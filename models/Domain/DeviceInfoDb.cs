using PublicTransportDevices.DbConnections; 
using PublicTransportDevices.Models.Data;

namespace PublicTransportDevices.Models.Domain; 

public class DeviceInfoDb
{
    private readonly ICommonDbConnection _dbConnection; 
    
    public DeviceInfoDb(ICommonDbConnection dbConnection)
    {
        _dbConnection = dbConnection; 
    }

    public System.Data.DataTable GetDeviceInfo()
    {
        var dt = new System.Data.DataTable(); 
        try
        {
            string sql = "select * from public.pt_device;"; 
            dt = _dbConnection.ExecuteSqlCommand(sql); 
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine($"Exception: {ex}"); 
        }
        return dt; 
    }

    public void InsertDeviceInfo(List<DeviceInfo> devices)
    {
        if (devices == null) 
            return; 
        try
        {
            System.Console.WriteLine($"devices: {devices.Count} in InsertDeviceInfo"); 
            foreach (var device in devices)
            {
                InsertDeviceInfo(device); 
            }
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine($"Exception: {ex}"); 
        }
    }

    public void InsertDeviceInfo(DeviceInfo device)
    {
        try
        {
            if (device != null && device.GeoCoordinate != null && device.DateTimeCreated != null)
            {
                string sql = $"select * from public.insert_into_pt_device_info('{device.Uid}', {device.GeoCoordinate.Latitude}, {device.GeoCoordinate.Longitude}, '{device.DateTimeCreated.ToString()}');"; 
                _dbConnection.ExecuteSqlCommand(sql); 
            }
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine($"Exception: {ex}"); 
        }
    }
}