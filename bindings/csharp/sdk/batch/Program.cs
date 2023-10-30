using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Dapr.Client;

var cronBindingName = "cron";
var sqlBindingName = "sqldb";

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Triggered by Dapr input binding
app.MapPost("/" + cronBindingName, async () =>
{
    Console.WriteLine("Processing batch...");
    string jsonFile = File.ReadAllText("../../../orders.json");
    var ordersArray = JsonSerializer.Deserialize<Orders>(jsonFile);
    Console.WriteLine(ordersArray);
    var client = new DaprClientBuilder().Build();
    foreach (Order order in ordersArray?.orders ?? new Order[] { })
    {
        var sqlText = $"insert into orders (orderid, customer, price) values ({order.OrderId}, '{order.Customer}', {order.Price});";
        var command = new Dictionary<string, string>()
        {
            {
                "sql",
                sqlText
            }
        };
        Console.WriteLine(sqlText);

        // Insert order using Dapr output binding via Dapr Client SDK
        await client.InvokeBindingAsync(bindingName: sqlBindingName, operation: "exec", data: "", metadata: command);
    }

    Console.WriteLine("Finished processing batch");
    return Results.Ok();
});

await app.RunAsync();

public record Order(
    [property: JsonPropertyName("orderid")] int OrderId,
    [property: JsonPropertyName("customer")] string Customer,
    [property: JsonPropertyName("price")] float Price
);

public record Orders([property: JsonPropertyName("orders")] Order[] orders);
