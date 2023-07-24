using System.Collections.Generic; 
using Microsoft.AspNetCore.Mvc;
using PublicTransportDevices.Models;
using PublicTransportDevices.Models.Data;
using PublicTransportDevices.Models.Domain; 

namespace PublicTransportDevices.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DeviceInfoController : ControllerBase
{
    private readonly ILogger<DeviceInfoController> _logger;
    private readonly DeviceInfoDb _deviceInfoDb; 

    public DeviceInfoController(ILogger<DeviceInfoController> logger, DeviceInfoDb deviceInfoDb)
    {
        _logger = logger;
        _deviceInfoDb = deviceInfoDb; 
    }

    [HttpGet(Name = "GetDeviceInfo")]
    public System.Data.DataTable Get()
    {
        var dt = new System.Data.DataTable(); 
        try
        {
            dt = _deviceInfoDb.GetDeviceInfo();
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine($"Exception: {ex}"); 
        }
        return dt; 
    }

    [HttpPost(Name = "PostDeviceInfo")]
    public void Post([FromBody] List<DeviceInfo> devices)  
    {
        if (devices == null) 
            return; 
        try
        {
            _deviceInfoDb.InsertDeviceInfo(devices); 
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine($"Exception: {ex}"); 
        }
    }
}
