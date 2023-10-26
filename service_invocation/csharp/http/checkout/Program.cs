// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var baseUrl = $"{Environment.GetEnvironmentVariable("BASE_URL") ?? "http://localhost"}:{Environment.GetEnvironmentVariable("DAPR_HTTP_PORT") ?? "3500"}";

// Console.WriteLine($"Dapr sidecar endpoint: {baseUrl}");

var httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

// Adding app id as part of the header: http endpoint http://localhost:3500/v1.0/invoke/order-processor/{method}
httpClient.DefaultRequestHeaders.Add("dapr-app-id", "order-processor");

for (int i = 1; i <= 10; i++) 
{ 
    var order = new Order(i);
    var orderJson = JsonSerializer.Serialize<Order>(order);
    var content = new StringContent(orderJson, Encoding.UTF8, "application/json");

    // Invoke a service method
    // Console.WriteLine($"http client base address {httpClient.BaseAddress}");
    Console.WriteLine("Send a order to order-processor service: " + order);
    var response = await httpClient.PostAsync($"{baseUrl}/orders", content);
    Console.WriteLine("Receive the result from order-processor service: " + await response.Content.ReadAsStringAsync());

    await Task.Delay(TimeSpan.FromSeconds(1));
}

public record Order([property: JsonPropertyName("orderId")] int OrderId);
