using Microsoft.EntityFrameworkCore;
using SimpleStocker.ProductApi.Context;
using SimpleStocker.ProductApi.Endpoints;
using SimpleStocker.ProductApi.MapsterConfig;
using SimpleStocker.ProductApi.Middlewares;
using SimpleStocker.ProductApi.RabbitMQ.RabbitMQSender;
using SimpleStocker.ProductApi.Repositories;
using SimpleStocker.ProductApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApiContext>(options =>
                   options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnextion")));
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddScoped<IRabbitMQMessageSender, RabbitMQMessageSender>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddOpenApi();
builder.Services.RegisterMapster();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

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

app.MapCategoryEndpoints()
   .MapProductEndpoints();
app.UseCors("AllowAll");
app.Run();
