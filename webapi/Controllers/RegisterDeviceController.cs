using System.Collections.Generic; 
using Microsoft.AspNetCore.Mvc;
using PublicTransportDevices.DbConnections; 
using PublicTransportDevices.Models;

namespace PublicTransportDevices.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RegisterDeviceController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<RegisterDeviceController> _logger;
    private readonly ICommonDbConnection _dbConnection; 

    public RegisterDeviceController(ILogger<RegisterDeviceController> logger, ICommonDbConnection dbConnection)
    {
        _logger = logger;
        _dbConnection = dbConnection; 
    }

    [HttpGet(Name = "GetDevice")]
    public IEnumerable<Device> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new Device
        {
            Uid = "ewhewoeh"
        })
        .ToArray();
    }

    [HttpPost(Name = "Register")]
    public void PostRegister([FromBody] List<Device> devices)  
    {
        if (devices == null) 
            return; 
        foreach (var device in devices)
        {
            string sql = $"insert into public.pt_device (device_uid, device_type_id) values ('{device.Uid}', {(int)device.DeviceType}); "; 
            _dbConnection.ExecuteSqlCommand(sql); 
        }
    }
}
