using PublicTransportDevices.DbConnections; 

namespace PublicTransportDevices.Models.Domain; 

public class DeviceInfoDb
{
    private readonly ICommonDbConnection _dbConnection; 
    
    public DeviceInfoDb(ICommonDbConnection dbConnection)
    {
        _dbConnection = dbConnection; 
    }

    public void InsertDeviceInfo(List<DeviceInfo> devices)
    {
        if (devices == null) 
            return; 
        try
        {
            foreach (var device in devices)
            {
                if (device != null && device.GeoCoordinate != null)
                {
                    string sql = $"select * from public.insert_into_pt_device_info('{device.Uid}', {device.GeoCoordinate.Latitude}, {device.GeoCoordinate.Longitude});"; 
                    _dbConnection.ExecuteSqlCommand(sql); 
                }
            }
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine($"Exception: {ex}"); 
        }
    }
}