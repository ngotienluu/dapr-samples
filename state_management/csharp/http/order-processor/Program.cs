using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var baseUrl = (Environment.GetEnvironmentVariable("BASE_URL") ?? "http://localhost") + ":" + (Environment.GetEnvironmentVariable("DAPR_HTTP_PORT") ?? "3500");
const string DAPR_STATE_STORE = "statestore";

var client = new HttpClient();
client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

for (int i = 1; i <= 10; i++)
{
    var order = new Order(i);
    var orderJson = JsonSerializer.Serialize(
        new[] {
            new {
                key = order.OrderId.ToString(),
                value = order
            }
        }
    );
    var state = new StringContent(orderJson, Encoding.UTF8, "application/json");

    // Save state into a state store
    await client.PostAsync($"{baseUrl}/v1.0/state/{DAPR_STATE_STORE}", state);
    Console.WriteLine("Saving Order: " + order);

    // Get state from a state store
    var response = await client.GetStringAsync($"{baseUrl}/v1.0/state/{DAPR_STATE_STORE}/{order.OrderId.ToString()}");
    Console.WriteLine("Getting Order: " + response);

    // Delete state from the state store
    await client.DeleteAsync($"{baseUrl}/v1.0/state/{DAPR_STATE_STORE}/{order.OrderId.ToString()}");
    Console.WriteLine("Deleting Order: " + order);

    await Task.Delay(TimeSpan.FromSeconds(1));
}

public record Order([property: JsonPropertyName("orderId")] int OrderId);