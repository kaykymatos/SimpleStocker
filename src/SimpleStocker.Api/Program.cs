using DbUp;
using SimpleStocker.Api.Context;
using SimpleStocker.Api.Endpoints;
using SimpleStocker.Api.Middlewares;
using SimpleStocker.Api.Models.Entities.Enums;
using SimpleStocker.Api.Models.ViewModels;
using SimpleStocker.Api.Repositories;
using SimpleStocker.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin() // Permite qualquer origem
              .AllowAnyMethod() // Permite qualquer método HTTP
              .AllowAnyHeader(); // Permite qualquer cabeçalho
    });
});
builder.Services.AddSingleton<DapperContext>();

//builder.Services.AddScoped<IDbConnection>(sp =>
//    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<ISaleItemRepository, SaleItemRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISaleService, SaleService>();

var app = builder.Build();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var upgrader = DeployChanges.To
    .PostgresqlDatabase(connectionString)
    .WithScriptsFromFileSystem("Migrations") // caminho para os arquivos .sql
    .LogToConsole()
    .Build();

var result = upgrader.PerformUpgrade();
if (builder.Environment.IsDevelopment())
{
    var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider;

    var clientRepository = scopeFactory.GetRequiredService<IClientService>();
    var productRepository = scopeFactory.GetRequiredService<IProductService>();
    var saleRepository = scopeFactory.GetRequiredService<ISaleService>();
    var categoryRepository = scopeFactory.GetRequiredService<ICategoryService>();

    await saleRepository.ClearDb();
    await clientRepository.ClearDb();
    await productRepository.ClearDb();
    await categoryRepository.ClearDb();
    var sale = await saleRepository.GetOneAsync(1);
    var client = await clientRepository.GetOneAsync(1);
    var product = await productRepository.GetOneAsync(1);
    var category = await categoryRepository.GetOneAsync(1);

    if (!sale.Success || !client.Success || !product.Success || !category.Success)
    {

        //Categories
        var cat1 = await categoryRepository.CreateAsync(new CategoryViewModel { Name = "Alimentos", Description = "Produtos alimentícios em geral" });
        var cat2 = await categoryRepository.CreateAsync(new CategoryViewModel { Name = "Limpeza", Description = "Produtos de limpeza doméstica" });
        var cat3 = await categoryRepository.CreateAsync(new CategoryViewModel { Name = "Eletrônicos", Description = "Aparelhos eletrônicos e acessórios" });
        var cat4 = await categoryRepository.CreateAsync(new CategoryViewModel { Name = "Vestuário", Description = "Roupas e vestuário em geral" });
        var cat5 = await categoryRepository.CreateAsync(new CategoryViewModel { Name = "Escritório", Description = "Material para escritório e papelaria" });
        var cat6 = await categoryRepository.CreateAsync(new CategoryViewModel { Name = "Bebidas", Description = "Bebidas alcoólicas e não alcoólicas" });

        //Products
        var prod1 = await productRepository.CreateAsync(new ProductViewModel
        {
            Name = "Arroz Tipo 1",
            Description = "Pacote de 5kg de arroz branco",
            Price = 25.90m,
            QuantityStock = 100,
            UnityOfMeasurement = "UN",
            CategoryId = cat1.Data.Id
        });

        var prod2 = await productRepository.CreateAsync(new ProductViewModel
        {
            Name = "Detergente Neutro",
            Description = "Frasco de 500ml",
            Price = 2.50m,
            QuantityStock = 200,
            UnityOfMeasurement = "UN",
            CategoryId = cat2.Data.Id
        });

        var prod3 = await productRepository.CreateAsync(new ProductViewModel
        {
            Name = "Fone de Ouvido Bluetooth",
            Description = "Com cancelamento de ruído",
            Price = 150.00m,
            QuantityStock = 50,
            UnityOfMeasurement = "UN",
            CategoryId = cat3.Data.Id
        });

        var prod4 = await productRepository.CreateAsync(new ProductViewModel
        {
            Name = "Camiseta Básica",
            Description = "Camiseta de algodão - tamanho M",
            Price = 39.90m,
            QuantityStock = 75,
            UnityOfMeasurement = "UN",
            CategoryId = cat4.Data.Id
        });

        var prod5 = await productRepository.CreateAsync(new ProductViewModel
        {
            Name = "Caderno Universitário",
            Description = "96 folhas capa dura",
            Price = 15.00m,
            QuantityStock = 120,
            UnityOfMeasurement = "UN",
            CategoryId = cat5.Data.Id
        });

        var prod6 = await productRepository.CreateAsync(new ProductViewModel
        {
            Name = "Suco de Laranja Natural",
            Description = "Garrafa 1L - sem conservantes",
            Price = 7.90m,
            QuantityStock = 60,
            UnityOfMeasurement = "UN",
            CategoryId = cat6.Data.Id
        });

        //Clients
        var client1 = await clientRepository.CreateAsync(new ClientViewModel
        {
            Name = "Ana Souza",
            Email = "ana.souza@email.com",
            PhoneNumer = "(11) 91234-5678",
            Address = "Rua das Flores",
            AddressNumber = "123",
            Active = true,
            BirthDate = new DateTime(1990, 5, 12)
        });

        var client2 = await clientRepository.CreateAsync(new ClientViewModel
        {
            Name = "Carlos Silva",
            Email = "carlos.silva@email.com",
            PhoneNumer = "(21) 99876-4321",
            Address = "Av. Paulista",
            AddressNumber = "456",
            Active = true,
            BirthDate = new DateTime(1985, 3, 22)
        });

        var client3 = await clientRepository.CreateAsync(new ClientViewModel
        {
            Name = "Juliana Ribeiro",
            Email = "juliana.ribeiro@email.com",
            PhoneNumer = "(31) 98888-7777",
            Address = "Rua Afonso Pena",
            AddressNumber = "789",
            Active = false,
            BirthDate = new DateTime(1992, 8, 9)
        });

        var client4 = await clientRepository.CreateAsync(new ClientViewModel
        {
            Name = "Fernando Lima",
            Email = "fernando.lima@email.com",
            PhoneNumer = "(41) 97777-1111",
            Address = "Travessa das Oliveiras",
            AddressNumber = "321",
            Active = true,
            BirthDate = new DateTime(1978, 12, 5)
        });

        var client5 = await clientRepository.CreateAsync(new ClientViewModel
        {
            Name = "Beatriz Martins",
            Email = "beatriz.martins@email.com",
            PhoneNumer = "(51) 92345-6789",
            Address = "Rua Dom Pedro II",
            AddressNumber = "147",
            Active = true,
            BirthDate = new DateTime(2000, 1, 18)
        });

        var client6 = await clientRepository.CreateAsync(new ClientViewModel
        {
            Name = "Rafael Costa",
            Email = "rafael.costa@email.com",
            PhoneNumer = "(61) 93456-7890",
            Address = "Av. Central",
            AddressNumber = "369",
            Active = false,
            BirthDate = new DateTime(1988, 7, 30)
        });

        //Sales
        var sale1 = await saleRepository.CreateAsync(new SaleViewModel
        {
            CustomerId = client1.Data.Id,
            PaymentMethod = EPaymentMethod.CreditCard,
            Status = ESaleStatus.Completed,
            Items =
            [
                new SaleItemViewModel
        {
            ProductId = prod1.Data.Id,
            Quantity = 2,
            UnityPrice = prod1.Data.Price
        },
        new SaleItemViewModel
        {
            ProductId = prod2.Data.Id,
            Quantity = 1,
            UnityPrice = prod2.Data.Price
        }
            ]
        });

        var sale2 = await saleRepository.CreateAsync(new SaleViewModel
        {
            CustomerId = client2.Data.Id,
            PaymentMethod = EPaymentMethod.Pix,
            Status = ESaleStatus.Pending,
            Items =
            [
                new SaleItemViewModel
        {
            ProductId = prod3.Data.Id,
            Quantity = 3,
            UnityPrice = prod3.Data.Price
        },
        new SaleItemViewModel
        {
            ProductId = prod5.Data.Id,
            Quantity = 2,
            UnityPrice = prod5.Data.Price
        }
            ]
        });

        var sale3 = await saleRepository.CreateAsync(new SaleViewModel
        {
            CustomerId = client3.Data.Id,
            PaymentMethod = EPaymentMethod.Cash,
            Status = ESaleStatus.Cancelled,
            Items =
            [
                new SaleItemViewModel
        {
            ProductId = prod4.Data.Id,
            Quantity = 1,
            UnityPrice = prod4.Data.Price
        },
        new SaleItemViewModel
        {
            ProductId = prod6.Data.Id,
            Quantity = 5,
            UnityPrice = prod6.Data.Price
        }
            ]
        });
    }
}

app.MapCategoryEndpoints()
    .MapClientEndpoints()
    .MapProductEndpoints()
    .MapSaleEndpoints();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.Run();
