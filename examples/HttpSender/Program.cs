﻿using System; 
using System.Collections.Generic; 
using System.Net.Http; 
using System.Net.Http.Json; 
using System.Text.Json; 
using System.Threading.Tasks; 
using PublicTransportDevices.Models.Data;

namespace PublicTransportDevices.Examples.HttpSender;

class Program
{
    static async Task Main(string[] args)
    {
        // Empty: 
        // 5000 - total: 9.68, average: 0.001936
        // 1000 - total: 1.920, average: 0.00192
        // so we can send around 500 empty requests per second 

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
            var list = new List<DeviceInfo>(); 
            list.Add(new DeviceInfo 
                {
                    // Uid = Guid.NewGuid().ToString(), 
                    Uid = "3eb20d9f-3350-4bd3-b343-d903d2e51cfb", 
                    GeoCoordinate = new GeoCoordinate
                    {
                        Latitude = rnd.NextDouble(),
                        Longitude = rnd.NextDouble()
                    }
                });
            tasks[i] = httpClient.PostAsJsonAsync(url, list); 
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