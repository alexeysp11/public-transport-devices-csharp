using System.Threading;
using System.Text;
using RabbitMQ.Client;
using PublicTransportDevices.Models;

// namespace PublicTransportDevices.Examples.RabbitMQSender;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "test_exchange", type: ExchangeType.Direct);

var dt1 = System.DateTime.Now; 

// Send asynchronously 
for (int i = 0; i < 100; i++)
{
    Thread thread = new Thread(() => 
    {
        string message = "Hello World! thread: " + Thread.CurrentThread.ManagedThreadId; 
        var body = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(exchange: "test_exchange",
                            routingKey: "",
                            basicProperties: null,
                            body: body);
        Thread.Sleep(5); 
        if (j == 99)
            Console.WriteLine($"Thread '{Thread.CurrentThread.ManagedThreadId}' finished"); 
    }); 
    thread.Start(); 
}

// Send synchronously 
// string message = "Hello World! ";
// var body = Encoding.UTF8.GetBytes(message);
// channel.BasicPublish(exchange: string.Empty,
//                      routingKey: "hello",
//                      basicProperties: null,
//                      body: body);
// Console.WriteLine($" [x] Sent {message}");

// 
var dt2 = System.DateTime.Now; 
var dif = dt2 - dt1; 
Console.WriteLine($"Started: {dt1}");
Console.WriteLine($"Executed: {dt2}");
Console.WriteLine($"Executed in: {dif.Seconds}:{dif.Milliseconds}");

// 
Console.ReadLine();
channel.Close(); 
connection.Close(); 

// Console.WriteLine(" Press [enter] to exit.");
// Console.ReadLine();
