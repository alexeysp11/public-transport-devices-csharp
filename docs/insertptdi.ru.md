# Регистрация нового события для конкретного устройства с занесением его в базу данных

Через RabbitMQ:

```C#
var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

string exchangeName = "ptd_exchange"; 

channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct);

// Send asynchronously 
for (int i = 0; i < 100; i++)
{
    Thread thread = new Thread(() => 
    {
        var maxj = 100; 
        var rnd = new System.Random(); 
        var dt1 = System.DateTime.Now; 
        for (int j = 0; j < maxj; j++)
        {
            // string message = "Hello World! thread: " + Thread.CurrentThread.ManagedThreadId; 
            string message = JsonSerializer.Serialize(new DeviceInfo 
                {
                    // Uid = Guid.NewGuid().ToString(), 
                    Uid = "3eb20d9f-3350-4bd3-b343-d903d2e51cfb", 
                    GeoCoordinate = new GeoCoordinate
                    {
                        Latitude = rnd.NextDouble(),
                        Longitude = rnd.NextDouble()
                    }, 
                    DateTimeCreated = System.DateTime.Now
                }); 
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: exchangeName,
                                routingKey: "",
                                basicProperties: null,
                                body: body);
            Thread.Sleep(5); 
            if (j == maxj - 1)
            {
                var dt2 = System.DateTime.Now; 
                var dif = dt2 - dt1; 
                Console.WriteLine($"Thread '{Thread.CurrentThread.ManagedThreadId}' finished (Started: {dt1}, Executed: {dt2}, Executed in: {dif.Seconds}:{dif.Milliseconds})"); 
            }
        }
    }); 
    thread.Start(); 
}

// 
Console.ReadLine();
channel.Close(); 
connection.Close(); 
```

Напрямую.

```C#
class Program
{
    static async Task Main(string[] args)
    {
        // Inserting: 
        // 1000 - total: 5:363
        // so we can make 166 to 200 insert requests per second. 

        // 
        var httpClient = new HttpClient(); 
        var requestCount = 1000; 
        var url = "https://localhost:7010/DeviceInfo"; 
        var tasks = new Task[requestCount]; 
        var rnd = new System.Random(); 
        for (int i = 0; i < requestCount; i++)
        {
            var deviceInfo = new DeviceInfo 
                {
                    Uid = "3eb20d9f-3350-4bd3-b343-d903d2e51cfb", 
                    GeoCoordinate = new GeoCoordinate
                    {
                        Latitude = rnd.NextDouble(),
                        Longitude = rnd.NextDouble()
                    }
                };
            tasks[i] = httpClient.PostAsJsonAsync(url, deviceInfo); 
        }
        
        // 
        var dt1 = System.DateTime.Now; 
        await Task.WhenAll(tasks); 

        // 
        var dt2 = System.DateTime.Now; 
        var dif = dt2 - dt1; 
        Console.WriteLine($"Started: {dt1}");
        Console.WriteLine($"Executed: {dt2}");
        Console.WriteLine($"Executed in: {dif.Seconds}:{dif.Milliseconds}");
    }
}
```
