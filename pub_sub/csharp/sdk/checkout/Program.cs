// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System;
using Dapr.Client;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

for (int i = 1; i <= 10; i++)
{
    var order = new Order(i);
    using var client = new DaprClientBuilder().Build();

    // publish an event/message using dapr pubsub
    await client.PublishEventAsync("orderpubsub", "orders", order);
    Console.WriteLine("publish data: " + order);
    
    await Task.Delay(TimeSpan.FromSeconds(1));
}

public record Order([property: JsonPropertyName("orderId")] int OrderId);
