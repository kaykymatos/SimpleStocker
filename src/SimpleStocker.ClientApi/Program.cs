using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SimpleStocker.ClientApi.Context;
using SimpleStocker.ClientApi.DTO;
using SimpleStocker.ClientApi.MapsterConfig;
using SimpleStocker.ClientApi.Middlewares;
using SimpleStocker.ClientApi.Repositories;
using SimpleStocker.ClientApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApiContext>(options =>
                   options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnextion")));
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.RegisterMapster();

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientService, ClientService>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.MapPost("clients", async ([FromBody] ClientDTO model, [FromServices] IClientService service) =>
{
    var response = await service.CreateAsync(model);
    return response.Success ? Results.Ok(response) : Results.BadRequest(response);

}).WithOpenApi(x =>
{
    x.Summary = "";
    x.Description = "";
    return x;
});

app.MapPut("clients", async ([FromQuery] long id, [FromBody] ClientDTO model, [FromServices] IClientService service) =>
{
    model.Id = id;
    var response = await service.UpdateAsync(id, model);
    return response.Success ? Results.Ok(response) : Results.BadRequest(response);
}).WithOpenApi(x =>
{
    x.Summary = "";
    x.Description = "";
    return x;
});

app.MapGet("clients/{id:long}", async ([FromRoute] long id, [FromServices] IClientService service) =>
{
    var response = await service.GetOneAsync(id);
    return response.Success ? Results.Ok(response) : Results.BadRequest(response);
}).WithOpenApi(x =>
{
    x.Summary = "Obtem um cliente pelo ID";
    x.Description = "Retorna os dados de um cliente específico a partir do ID informado na URL.";
    x.Parameters = new List<OpenApiParameter>
    {
        new OpenApiParameter
        {
            Name = "id",
            In = ParameterLocation.Path,
            Required = true,
            Description = "ID do cliente",
            Schema = new OpenApiSchema { Type = "integer", Format = "int64" }
        }
    };
    return x;
});

app.MapGet("clients", async ([FromServices] IClientService service) =>
{
    var response = await service.GetAllAsync();
    return response.Success ? Results.Ok(response) : Results.BadRequest(response);
}).WithOpenApi(x =>
{
    x.Summary = "";
    x.Description = "";
    return x;
});

app.MapDelete("clients/{id:long}", async ([FromRoute] long id, [FromServices] IClientService service) =>
{
    var response = await service.DeleteAsync(id);
    return response.Success ? Results.Ok(response) : Results.BadRequest(response);
}).WithOpenApi(x =>
{
    x.Summary = "Deleta um cliente pelo ID";
    x.Description = "Deleta um cliente a partir do ID informado na URL.";
    x.Parameters = new List<OpenApiParameter>
    {
        new OpenApiParameter
        {
            Name = "id",
            In = ParameterLocation.Path,
            Required = true,
            Description = "ID do cliente",
            Schema = new OpenApiSchema { Type = "integer", Format = "int64" }
        }
    };
    return x;
});

app.Run();
