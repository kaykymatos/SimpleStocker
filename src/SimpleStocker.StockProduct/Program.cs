using Microsoft.EntityFrameworkCore;
using SimpleStocker.StockConsumer;
using SimpleStocker.StockConsumer.Context;
using SimpleStocker.StockConsumer.Repositories;

var builder = Host.CreateApplicationBuilder(args);


builder.Services.AddDbContext<ApiContext>(options =>
                   options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnextion")));
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddHostedService<CreateStockProductWorker>();
builder.Services.AddHostedService<UpdateStockProductWorker>();
builder.Services.AddHostedService<SellProductStockWorker>();

var host = builder.Build();
host.Run();
