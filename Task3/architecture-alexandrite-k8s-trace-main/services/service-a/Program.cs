using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource
        .AddService(serviceName: "OrdersService"))
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .AddConsoleExporter()
        .AddOtlpExporter(options =>
        {
            options.Endpoint = new Uri(builder.Configuration["TRACING_URL"] ?? throw new InvalidOperationException("TRACING_URL is not configured."));
        }));

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
