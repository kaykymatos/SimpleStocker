using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using SimpleStocker.SaleApi.DTO;
using SimpleStocker.SaleApi.Services;

namespace SimpleStocker.SaleApi.Endpoints
{
    public static class SaleItemndpoints
    {
        public static WebApplication MapSaleItemEndpoints(this WebApplication app)
        {
            app.MapPost("saleitems", async ([FromBody] SaleItemDTO model, [FromServices] ISaleItemService service) =>
            {
                var response = await service.CreateAsync(model);
                return response.Success ? Results.Ok(response) : Results.BadRequest(response);

            }).WithOpenApi(x =>
            {
                x.Summary = "";
                x.Description = "";
                return x;
            });

            app.MapPut("saleitems/{id:long}", async ([FromRoute] long id, [FromBody] SaleItemDTO model, [FromServices] ISaleItemService service) =>
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

            app.MapGet("saleitems/{id:long}", async ([FromRoute] long id, [FromServices] ISaleItemService service) =>
            {
                var response = await service.GetOneAsync(id);
                return response.Success ? Results.Ok(response) : Results.BadRequest(response);
            }).WithOpenApi(x =>
            {
                x.Summary = "Get SaleItem by ID";
                x.Description = "Returns the data of a specific SaleItem based on the ID provided in the URL.";
                x.Parameters = new List<OpenApiParameter>
                            {
                                    new OpenApiParameter
                                    {
                                        Name = "id",
                                        In = ParameterLocation.Path,
                                        Required = true,
                                        Description = "SaleItem ID",
                                        Schema = new OpenApiSchema { Type = "integer", Format = "int64" }
                                    }
                            };
                return x;
            });

            app.MapGet("saleitems", async ([FromServices] ISaleItemService service) =>
            {
                var response = await service.GetAllAsync();
                return response.Success ? Results.Ok(response) : Results.BadRequest(response);
            }).WithOpenApi(x =>
            {
                x.Summary = "";
                x.Description = "";
                return x;
            });

            app.MapDelete("saleitems/{id:long}", async ([FromRoute] long id, [FromServices] ISaleItemService service) =>
            {
                var response = await service.DeleteAsync(id);
                return response.Success ? Results.Ok(response) : Results.BadRequest(response);
            }).WithOpenApi(x =>
            {
                x.Summary = "Delete SaleItem by ID";
                x.Description = "Deletes a SaleItem based on the ID provided in the URL.";
                x.Parameters = new List<OpenApiParameter>
                        {
                                new OpenApiParameter
                                {
                                    Name = "id",
                                    In = ParameterLocation.Path,
                                    Required = true,
                                    Description = "SaleItem ID",
                                    Schema = new OpenApiSchema { Type = "integer", Format = "int64" }
                                }
                        };
                return x;
            });
            return app;
        }

    }
}
