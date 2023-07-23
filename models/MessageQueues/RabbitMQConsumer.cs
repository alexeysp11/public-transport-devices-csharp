using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PublicTransportDevices.Models.MessageQueues; 

public class RabbitMQConsumer
{
    private readonly IConnection _connection; 
    private readonly IModel _channel; 
    private readonly Timer _timer; 

    public RabbitMQConsumer()
    {
        System.Console.WriteLine("RabbitMQConsumer"); 
        var factory = new ConnectionFactory { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "hello",
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);
        _timer = new Timer(OnTimerElapsed, null, System.TimeSpan.Zero, System.TimeSpan.FromSeconds(1)); 
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
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($" [x] Received {message}");
        };
        _channel.BasicConsume(queue: "hello",
                            autoAck: true,
                            consumer: consumer);
    }
}