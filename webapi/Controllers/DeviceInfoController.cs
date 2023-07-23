using System.Collections.Generic; 
using Microsoft.AspNetCore.Mvc;
using PublicTransportDevices.DbConnections; 
using PublicTransportDevices.Models;

namespace PublicTransportDevices.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DeviceInfoController : ControllerBase
{
    private readonly ILogger<DeviceInfoController> _logger;
    private readonly ICommonDbConnection _dbConnection; 

    public DeviceInfoController(ILogger<DeviceInfoController> logger, ICommonDbConnection dbConnection)
    {
        _logger = logger;
        _dbConnection = dbConnection; 
    }

    [HttpPost(Name = "DeviceInfo")]
    public void Post([FromBody] List<DeviceInfo> devices)  
    {
        if (devices == null) 
            return; 
        foreach (var device in devices)
        {
            string sql = $"select * from public.insert_into_pt_device_info('{device.Uid}', {device.GeoCoordinate.Latitude}, {device.GeoCoordinate.Longitude});"; 
            _dbConnection.ExecuteSqlCommand(sql); 
        }
    }
}
