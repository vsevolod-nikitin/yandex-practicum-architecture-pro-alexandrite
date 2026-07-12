var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/orders", () =>
{
    return Results.Ok(new[]
    {
        new { Id = 1, Status = "Created", Customer = "Alice" },
        new { Id = 2, Status = "Processing", Customer = "Bob" }
    });
})
.WithName("GetOrders");

app.Run();
