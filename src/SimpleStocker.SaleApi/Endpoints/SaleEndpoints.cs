using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using SimpleStocker.SaleApi.DTO;
using SimpleStocker.SaleApi.RabbitMQ.RabbitMQModels;
using SimpleStocker.SaleApi.RabbitMQ.RabbitMQSender;
using SimpleStocker.SaleApi.Services;

namespace SimpleStocker.SaleApi.Endpoints
{
    public static class SaleEndpoints
    {
        private const string EMAIL_QUEUE = "EmailQueue";
        private const string STOCK_QUEUE = "SaleQueue";
        public static WebApplication MapSaleEndpoints(this WebApplication app)
        {
            app.MapPost("sales", async ([FromBody] SaleDTO model, [FromServices] ISaleService service, [FromServices] IRabbitMQMessageSender rabbitMQMessageSender, CancellationToken token) =>
            {
                var response = await service.CreateAsync(model);
                if (response.Success)
                {
                    rabbitMQMessageSender.SendMessage(response.Data.Adapt<SaleRabbitMQModel>(), EMAIL_QUEUE);
                    rabbitMQMessageSender.SendMessage(response.Data.Adapt<SaleRabbitMQModel>(), STOCK_QUEUE);
                }
                return response.Success ? Results.Ok(response) : Results.BadRequest(response);

            }).WithOpenApi(x =>
            {
                x.Summary = "";
                x.Description = "";
                return x;
            });

            app.MapPut("sales/{id:long}", async ([FromRoute] long id, [FromBody] SaleDTO model, [FromServices] ISaleService service) =>
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

            app.MapGet("sales/{id:long}", async ([FromRoute] long id, [FromServices] ISaleService service) =>
            {
                var response = await service.GetOneAsync(id);
                return response.Success ? Results.Ok(response) : Results.BadRequest(response);
            }).WithOpenApi(x =>
            {
                x.Summary = "Get Sale by ID";
                x.Description = "Returns the data of a specific Sale based on the ID provided in the URL.";
                x.Parameters = new List<OpenApiParameter>
                            {
                                    new OpenApiParameter
                                    {
                                        Name = "id",
                                        In = ParameterLocation.Path,
                                        Required = true,
                                        Description = "Sale ID",
                                        Schema = new OpenApiSchema { Type = "integer", Format = "int64" }
                                    }
                            };
                return x;
            });

            app.MapGet("sales", async ([FromServices] ISaleService service) =>
            {
                var response = await service.GetAllAsync();
                return response.Success ? Results.Ok(response) : Results.BadRequest(response);
            }).WithOpenApi(x =>
            {
                x.Summary = "";
                x.Description = "";
                return x;
            });

            app.MapDelete("sales/{id:long}", async ([FromRoute] long id, [FromServices] ISaleService service) =>
            {
                var response = await service.DeleteAsync(id);
                return response.Success ? Results.Ok(response) : Results.BadRequest(response);
            }).WithOpenApi(x =>
            {
                x.Summary = "Delete Sale by ID";
                x.Description = "Deletes a Sale based on the ID provided in the URL.";
                x.Parameters = new List<OpenApiParameter>
                        {
                                new OpenApiParameter
                                {
                                    Name = "id",
                                    In = ParameterLocation.Path,
                                    Required = true,
                                    Description = "Sale ID",
                                    Schema = new OpenApiSchema { Type = "integer", Format = "int64" }
                                }
                        };
                return x;
            });

            app.MapDelete("sales/batch", async ([FromBody] List<long> ids, [FromServices] ISaleService service) =>
            {
                var response = await service.DeleteManyAsync(ids);
                return response.Success ? Results.Ok(response) : Results.BadRequest(response);
            }).WithOpenApi(x =>
            {
                x.Summary = "Delete multiple sales";
                x.Description = "Deletes multiple sales based on a list of IDs sent in the request body.";
                return x;
            });
            return app;
        }
    }
}
