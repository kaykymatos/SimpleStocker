using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using SimpleStocker.InventoryApi.Services;

namespace SimpleStocker.InventoryApi.Endpoints
{
    public static class InventoryEndpoints
    {
        public static WebApplication MapInventoryEndpoints(this WebApplication app)
        {

            app.MapGet("inventory/{id:long}", async ([FromRoute] long id, [FromServices] IInventoryService service) =>
            {
                var response = await service.GetOneAsync(id);
                return response.Success ? Results.Ok(response) : Results.BadRequest(response);
            }).WithOpenApi(x =>
            {
                x.Summary = "Get Inventory by ID";
                x.Description = "Returns the data of a specific Inventory based on the ID provided in the URL.";
                x.Parameters = new List<OpenApiParameter>
    {
        new OpenApiParameter
        {
            Name = "id",
            In = ParameterLocation.Path,
            Required = true,
            Description = "Inventory ID",
            Schema = new OpenApiSchema { Type = "integer", Format = "int64" }
        }
    };
                return x;
            });

            app.MapGet("inventory", async ([FromServices] IInventoryService service) =>
            {
                var response = await service.GetAllAsync();
                return response.Success ? Results.Ok(response) : Results.BadRequest(response);
            }).WithOpenApi(x =>
            {
                x.Summary = "";
                x.Description = "";
                return x;
            });
            return app;
        }
    }
}
