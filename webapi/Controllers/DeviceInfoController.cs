using System.Collections.Generic; 
using Microsoft.AspNetCore.Mvc;
using PublicTransportDevices.Models;
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

    [HttpPost(Name = "DeviceInfo")]
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
