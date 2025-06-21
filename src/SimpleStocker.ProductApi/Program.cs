using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SimpleStocker.CategoryApi.Endpoints;
using SimpleStocker.CategoryApi.Repositories;
using SimpleStocker.CategoryApi.Services;
using SimpleStocker.ProductApi.Context;
using SimpleStocker.ProductApi.DTO;
using SimpleStocker.ProductApi.Endpoints;
using SimpleStocker.ProductApi.MapsterConfig;
using SimpleStocker.ProductApi.Middlewares;
using SimpleStocker.ProductApi.Repositories;
using SimpleStocker.ProductApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApiContext>(options =>
                   options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnextion")));
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.RegisterMapster();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

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

app.MapCategoryndpoints()
   .MapProductEndpoints();

app.Run();
