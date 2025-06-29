using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using SimpleStocker.SaleApi.DTO;
using SimpleStocker.SaleApi.Producers;
using SimpleStocker.SaleApi.Services;

namespace SimpleStocker.SaleApi.Endpoints
{
    public static class SaleEndpoints
    {
        private const string EMAIL_TOPIC = "email-sender";
        private const string STOCK_TOPIC = "sale-topic";
        public static WebApplication MapSaleEndpoints(this WebApplication app)
        {
            app.MapPost("sales", async ([FromBody] SaleDTO model, [FromServices] ISaleService service, CancellationToken token) =>
            {
                var response = await service.CreateAsync(model);

                await Task.WhenAll(
                    BasicProducer.ProduceMessage(EMAIL_TOPIC, token, response.Data),
                    BasicProducer.ProduceMessage(STOCK_TOPIC, token, response.Data)
                    );


                return response.Success ? Results.Ok(response) : Results.BadRequest(response);

            }).WithOpenApi(x =>
            {
                x.Summary = "";
                x.Description = "";
                return x;
            });

            app.MapPut("sales", async ([FromQuery] long id, [FromBody] SaleDTO model, [FromServices] ISaleService service) =>
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
                x.Summary = "Obtem Sale pelo ID";
                x.Description = "Retorna os dados de Sale específico a partir do ID informado na URL.";
                x.Parameters = new List<OpenApiParameter>
                            {
                                new OpenApiParameter
                                {
                                    Name = "id",
                                    In = ParameterLocation.Path,
                                    Required = true,
                                    Description = "ID do Sale",
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
                x.Summary = "Deleta Sale pelo ID";
                x.Description = "Deleta Sale a partir do ID informado na URL.";
                x.Parameters = new List<OpenApiParameter>
                        {
                            new OpenApiParameter
                            {
                                Name = "id",
                                In = ParameterLocation.Path,
                                Required = true,
                                Description = "ID do Sale",
                                Schema = new OpenApiSchema { Type = "integer", Format = "int64" }
                            }
                        };
                return x;
            });
            return app;
        }
    }
}
