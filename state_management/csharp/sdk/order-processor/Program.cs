// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System.Text.Json.Serialization;
using Dapr.Client;

string DAPR_STORE_NAME = "statestore";
using var client = new DaprClientBuilder().Build();

for (int i = 0; i < 10; i++)
{
    var order = new Order(i);
    // Save state into the state store
    await client.SaveStateAsync(DAPR_STORE_NAME, order.OrderId.ToString(), order.ToString());
    Console.WriteLine("Saving order: " + order);

    // Get state from the state store
    var result = await client.GetStateAsync<string>(DAPR_STORE_NAME, order.OrderId.ToString());
    Console.WriteLine("Getting order: " + result);

    // Delete state from the state store
    // await client.DeleteStateAsync(DAPR_STORE_NAME, order.OrderId.ToString());
    // Console.WriteLine("Deleting Order: " + order);

    await Task.Delay(TimeSpan.FromSeconds(1));
}

public record Order([property: JsonPropertyName("orderId")] int OrderId);


