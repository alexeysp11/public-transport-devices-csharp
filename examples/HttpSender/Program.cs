using System; 
using System.Net.Http; 
using System.Net.Http.Json; 
using System.Text.Json; 
using System.Threading.Tasks; 
using PublicTransportDevices.Models;

namespace PublicTransportDevices.Examples.HttpSender;

class Program
{
    static async Task Main(string[] args)
    {
        // 5000 - total: 9.68, average: 0.001936
        // 1000 - total: 1.920, average: 0.00192
        // so we can send 500 requests per second 
        var httpClient = new HttpClient(); 
        var requestCount = 1000; 
        var url = "https://localhost:7010/WeatherForecast"; 
        var customer = new Customer { Uid = Guid.NewGuid().ToString(), CompanyName = "CompanyName", ContactName = "ContactName" };
        var tasks = new Task[requestCount]; 
        for (int i = 0; i < requestCount; i++)
        {
            tasks[i] = httpClient.PostAsJsonAsync(url, new Customer 
                { 
                    Uid = Guid.NewGuid().ToString(), 
                    CompanyName = "CompanyName", 
                    ContactName = "ContactName" 
                }); 
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

        // 
        // Console.WriteLine($"tasks[0].Headers: {tasks[0].Result.Headers}"); 
        // string responseBody = await tasks[0].Result.Content.ReadAsStringAsync();
        // Console.WriteLine($"tasks[0].Content: {responseBody}"); 
        // Console.WriteLine($"json: {json}"); 
    }
}