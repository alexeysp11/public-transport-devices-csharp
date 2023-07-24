using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using PublicTransportDevices.Models.Data;
using PublicTransportDevices.Models.Domain; 

namespace PublicTransportDevices.Models.MessageQueues; 

public class RabbitMQConsumer
{
    private readonly DeviceInfoDb _deviceInfoDb; 
    private readonly IConnection _connection; 
    private readonly IModel _channel; 
    private readonly Timer _timer; 

    public RabbitMQConsumer(DeviceInfoDb deviceInfoDb)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "hello",
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);
        _timer = new Timer(OnTimerElapsed, null, System.TimeSpan.Zero, System.TimeSpan.FromSeconds(1)); 
        _deviceInfoDb = deviceInfoDb; 
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask; 
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer.Dispose(); 
        _channel.Close(); 
        _connection.Close(); 
        return Task.CompletedTask; 
    }

    private void OnTimerElapsed(object state)
    {
        try
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                DeviceInfo di = JsonSerializer.Deserialize<DeviceInfo>(message);
                _deviceInfoDb.InsertDeviceInfo(di); 
            };
            _channel.BasicConsume(queue: "hello",
                                autoAck: true,
                                consumer: consumer);
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine($"Exception: {ex}"); 
        }
    }
}