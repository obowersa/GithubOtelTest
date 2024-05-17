//using Grafana.OpenTelemetry;

var builder = WebApplication.CreateBuilder(args);
/*
builder.Services.AddOpenTelemetry()
    .WithMetrics(builder => builder.UseGrafana())
    .WithTracing(builder => builder.UseGrafana());
*/
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();