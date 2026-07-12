var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("OrdersService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ORDERS_SERVICE_URL"] ?? throw new InvalidOperationException("ORDERS_SERVICE_URL is not configured."));
});

var app = builder.Build();

app.MapGet("/calculations", async (IHttpClientFactory httpClientFactory) =>
{
    var httpClient = httpClientFactory.CreateClient("OrdersService");
    var response = await httpClient.GetAsync("/orders");

    response.EnsureSuccessStatusCode();

    var orders = await response.Content.ReadAsStringAsync();

    return Results.Ok(orders);
})
.WithName("GetCalculations");

app.Run();
