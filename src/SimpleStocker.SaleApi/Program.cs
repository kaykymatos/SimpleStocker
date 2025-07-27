
using Microsoft.EntityFrameworkCore;
using SimpleStocker.SaleApi.Context;
using SimpleStocker.SaleApi.Endpoints;
using SimpleStocker.SaleApi.MapsterConfig;
using SimpleStocker.SaleApi.Middlewares;
using SimpleStocker.SaleApi.RabbitMQ.RabbitMQSender;
using SimpleStocker.SaleApi.Repositories;
using SimpleStocker.SaleApi.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApiContext>(options =>
                   options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnextion")));
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.RegisterMapster();
builder.Services.AddSingleton<IRabbitMQMessageSender, RabbitMQMessageSender>();

builder.Services.AddScoped<ISaleItemRepository, SaleItemRepository>();
builder.Services.AddScoped<ISaleItemService, SaleItemService>();

builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ISaleService, SaleService>();

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

app.MapSaleEndpoints();
//.MapSaleItemEndpoints();

app.Run();
