using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Define ambiente (Development ou Production)
var environment = builder.Environment.EnvironmentName;

// Carrega os arquivos corretos
builder.Configuration
    .AddJsonFile($"ocelot.{environment}.json", optional: false, reloadOnChange: true);

builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

await app.UseOcelot();

app.Run();