using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using SimpleStocker.ProductApi.DTO;
using SimpleStocker.ProductApi.Factories;
using SimpleStocker.ProductApi.RabbitMQ.RabbitMQModels;
using SimpleStocker.ProductApi.RabbitMQ.RabbitMQSender;
using SimpleStocker.ProductApi.Services;

namespace SimpleStocker.ProductApi.Endpoints
{
    public static class ProductEndpoints
    {
        public static WebApplication MapProductEndpoints(this WebApplication app)
        {
            app.MapPost("products", async ([FromBody] ProductDTO model, [FromServices] IProductService service, [FromServices] IRabbitMQMessageSender rabbitMQMessageSender, CancellationToken token) =>
            {
                var response = await service.CreateAsync(model);
                if (response.Success)
                    rabbitMQMessageSender.SendMessage(response.Data.Adapt<ProductRabbitMQModel>(), "CreateProductQueue");

                return response.Success ? Results.Ok(response) : Results.BadRequest(response);

            }).WithOpenApi(x =>
            {
                x.Summary = "";
                x.Description = "";
                return x;
            });

            app.MapPut("products/{id:long}", async ([FromRoute] long id, [FromBody] ProductDTO model, [FromServices] IProductService service, [FromServices] IRabbitMQMessageSender rabbitMQMessageSender, CancellationToken token) =>
            {
                model.Id = id;
                var response = await service.UpdateAsync(id, model);
                if (response.Success)
                    rabbitMQMessageSender.SendMessage(response.Data.Adapt<ProductRabbitMQModel>(), "UpdateProductQueue");

                return response.Success ? Results.Ok(response) : Results.BadRequest(response);
            }).WithOpenApi(x =>
            {
                x.Summary = "";
                x.Description = "";
                return x;
            });

            app.MapGet("products/{id:long}", async ([FromRoute] long id, [FromServices] IProductService service, IConfiguration config) =>
            {
                var response = await service.GetOneAsync(id);

                var httpClientFactoryService = new HttpClientFactory(new HttpClient() { BaseAddress = new Uri(config["ExternalServicesUrls:InventorySerivce"]) });
                ApiResponse<List<InventoryDTO>> inventoryData = new ApiResponse<List<InventoryDTO>>();
                try
                {
                    inventoryData = await httpClientFactoryService.PostAsync<List<InventoryDTO>>(
                        "/inventory/get-inventory-by-product-id-list",
                        new List<long> { id }
                    );
                    response.Data.QuantityStock = inventoryData.Data.First().Quantity;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                return response.Success ? Results.Ok(response) : Results.BadRequest(response);
            }).WithOpenApi(x =>
            {
                x.Summary = "Get a Product by ID";
                x.Description = "Returns the data of a specific Product based on the ID provided in the URL.";
                x.Parameters = new List<OpenApiParameter>
                            {
                                    new OpenApiParameter
                                    {
                                        Name = "id",
                                        In = ParameterLocation.Path,
                                        Required = true,
                                        Description = "Product ID",
                                        Schema = new OpenApiSchema { Type = "integer", Format = "int64" }
                                    }
                            };
                return x;
            });

            app.MapGet("products", async (
                [FromServices] IProductService service,
                [FromServices] IConfiguration config) =>
            {
                var response = await service.GetAllAsync();

                var httpClientFactoryService = new HttpClientFactory(new HttpClient() { BaseAddress = new Uri(config["ExternalServicesUrls:InventorySerivce"]) });
                ApiResponse<List<InventoryDTO>> inventoryData = new ApiResponse<List<InventoryDTO>>();
                try
                {
                    inventoryData = await httpClientFactoryService.PostAsync<List<InventoryDTO>>(
                        "/inventory/get-inventory-by-product-id-list",
                        response.Data.Select(x => x.Id).ToList()
                    );
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

                foreach (var item in response.Data)
                    item.QuantityStock = inventoryData.Data.First(x => x.ProductId == item.Id).Quantity;

                return response.Success ? Results.Ok(response) : Results.BadRequest(response);
            })
                .WithOpenApi(x =>
                {
                    x.Summary = "";
                    x.Description = "";
                    return x;
                });

            app.MapDelete("products/{id:long}", async ([FromRoute] long id, [FromServices] IProductService service) =>
            {
                var response = await service.DeleteAsync(id);
                return response.Success ? Results.Ok(response) : Results.BadRequest(response);
            }).WithOpenApi(x =>
            {
                x.Summary = "Delete a Product by ID";
                x.Description = "Deletes a Product based on the ID provided in the URL.";
                x.Parameters = new List<OpenApiParameter>
                        {
                                new OpenApiParameter
                                {
                                    Name = "id",
                                    In = ParameterLocation.Path,
                                    Required = true,
                                    Description = "Product ID",
                                    Schema = new OpenApiSchema { Type = "integer", Format = "int64" }
                                }
                        };
                return x;
            });
            app.MapDelete("products/batch", async ([FromBody] List<long> ids, [FromServices] IProductService service) =>
            {
                var response = await service.DeleteManyAsync(ids);
                return response.Success ? Results.Ok(response) : Results.BadRequest(response);
            }).WithOpenApi(x =>
            {
                x.Summary = "Delete multiple products";
                x.Description = "Deletes multiple products based on a list of IDs sent in the request body.";
                return x;
            });
            return app;
        }
    }
}
