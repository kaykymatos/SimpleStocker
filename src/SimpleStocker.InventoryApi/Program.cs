
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SimpleStocker.InventoryApi.Context;
using SimpleStocker.InventoryApi.MapsterConfig;
using SimpleStocker.InventoryApi.Middlewares;
using SimpleStocker.InventoryApi.Repositories;
using SimpleStocker.InventoryApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApiContext>(options =>
                   options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnextion")));
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.RegisterMapster();

builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IInventoryService, InventoryService>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
    app.UseCors("AllowAll");
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("inventory/{id:long}", async ([FromRoute] long id, [FromServices] IInventoryService service) =>
{
    var response = await service.GetOneAsync(id);
    return response.Success ? Results.Ok(response) : Results.BadRequest(response);
}).WithOpenApi(x =>
{
    x.Summary = "Obtem um Inventory pelo ID";
    x.Description = "Retorna os dados de um Inventory espec�fico a partir do ID informado na URL.";
    x.Parameters = new List<OpenApiParameter>
    {
        new OpenApiParameter
        {
            Name = "id",
            In = ParameterLocation.Path,
            Required = true,
            Description = "ID do Inventory",
            Schema = new OpenApiSchema { Type = "integer", Format = "int64" }
        }
    };
    return x;
});

app.MapGet("inventory", async ([FromServices] IInventoryService service) =>
{
    var response = await service.GetAllAsync();
    return response.Success ? Results.Ok(response) : Results.BadRequest(response);
}).WithOpenApi(x =>
{
    x.Summary = "";
    x.Description = "";
    return x;
});


app.Run();
