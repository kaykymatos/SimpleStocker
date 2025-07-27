using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using SimpleStocker.ProductApi.DTO;
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

                rabbitMQMessageSender.SendMessage(response.Data.Adapt<ProductRabbitMQModel>(), "CreateProductQueue");

                return response.Success ? Results.Ok(response) : Results.BadRequest(response);

            }).WithOpenApi(x =>
            {
                x.Summary = "";
                x.Description = "";
                return x;
            });

            app.MapPut("products", async ([FromQuery] long id, [FromBody] ProductDTO model, [FromServices] IProductService service, [FromServices] IRabbitMQMessageSender rabbitMQMessageSender, CancellationToken token) =>
            {
                model.Id = id;
                var response = await service.UpdateAsync(id, model);
                rabbitMQMessageSender.SendMessage(response.Data.Adapt<ProductRabbitMQModel>(), "UpdateProductQueue");

                return response.Success ? Results.Ok(response) : Results.BadRequest(response);
            }).WithOpenApi(x =>
            {
                x.Summary = "";
                x.Description = "";
                return x;
            });

            app.MapGet("products/{id:long}", async ([FromRoute] long id, [FromServices] IProductService service) =>
            {
                var response = await service.GetOneAsync(id);
                return response.Success ? Results.Ok(response) : Results.BadRequest(response);
            }).WithOpenApi(x =>
            {
                x.Summary = "Obtem um Producte pelo ID";
                x.Description = "Retorna os dados de um Producte específico a partir do ID informado na URL.";
                x.Parameters = new List<OpenApiParameter>
                            {
                                new OpenApiParameter
                                {
                                    Name = "id",
                                    In = ParameterLocation.Path,
                                    Required = true,
                                    Description = "ID do Producte",
                                    Schema = new OpenApiSchema { Type = "integer", Format = "int64" }
                                }
                            };
                return x;
            });

            app.MapGet("products", async ([FromServices] IProductService service) =>
            {
                var response = await service.GetAllAsync();
                return response.Success ? Results.Ok(response) : Results.BadRequest(response);
            }).WithOpenApi(x =>
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
                x.Summary = "Deleta um Producte pelo ID";
                x.Description = "Deleta um Producte a partir do ID informado na URL.";
                x.Parameters = new List<OpenApiParameter>
                        {
                            new OpenApiParameter
                            {
                                Name = "id",
                                In = ParameterLocation.Path,
                                Required = true,
                                Description = "ID do Producte",
                                Schema = new OpenApiSchema { Type = "integer", Format = "int64" }
                            }
                        };
                return x;
            });
            return app;
        }
    }
}
