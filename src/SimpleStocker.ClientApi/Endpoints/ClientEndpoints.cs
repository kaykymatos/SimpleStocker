using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using SimpleStocker.ClientApi.DTO;
using SimpleStocker.ClientApi.Services;
public static class ClientEndpoints
{
    public static WebApplication MapClientEndpoints(this WebApplication app)
    {
        app.MapPost("clients", async ([FromBody] ClientDTO model, [FromServices] IClientService service) =>
        {
            var response = await service.CreateAsync(model);
            return response.Success ? Results.Ok(response) : Results.BadRequest(response);

        }).WithOpenApi(x =>
        {
            x.Summary = "";
            x.Description = "";
            return x;
        });

        app.MapPut("clients/{id:long}", async ([FromRoute] long id, [FromBody] ClientDTO model, [FromServices] IClientService service) =>
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

        app.MapGet("clients/{id:long}", async ([FromRoute] long id, [FromServices] IClientService service) =>
        {
            var response = await service.GetOneAsync(id);
            return response.Success ? Results.Ok(response) : Results.BadRequest(response);
        }).WithOpenApi(x =>
        {
            x.Summary = "Get a client by ID";
            x.Description = "Returns the data of a specific client by the ID provided in the URL.";
            x.Parameters = new List<OpenApiParameter>
    {
        new OpenApiParameter
        {
            Name = "id",
            In = ParameterLocation.Path,
            Required = true,
            Description = "Client ID",
            Schema = new OpenApiSchema { Type = "integer", Format = "int64" }
        }
    };
            return x;
        });

        app.MapGet("clients", async ([FromServices] IClientService service) =>
        {
            var response = await service.GetAllAsync();
            return response.Success ? Results.Ok(response) : Results.BadRequest(response);
        }).WithOpenApi(x =>
        {
            x.Summary = "";
            x.Description = "";
            return x;
        });

        app.MapDelete("clients/{id:long}", async ([FromRoute] long id, [FromServices] IClientService service) =>
        {
            var response = await service.DeleteAsync(id);
            return response.Success ? Results.Ok(response) : Results.BadRequest(response);
        }).WithOpenApi(x =>
        {
            x.Summary = "Delete a client by ID";
            x.Description = "Deletes a client by the ID provided in the URL.";
            x.Parameters = new List<OpenApiParameter>
    {
        new OpenApiParameter
        {
            Name = "id",
            In = ParameterLocation.Path,
            Required = true,
            Description = "Client ID",
            Schema = new OpenApiSchema { Type = "integer", Format = "int64" }
        }
    };
            return x;
        });

        app.MapDelete("clients/batch", async ([FromBody] List<long> ids, [FromServices] IClientService service) =>
        {
            var response = await service.DeleteManyAsync(ids);
            return response.Success ? Results.Ok(response) : Results.BadRequest(response);
        }).WithOpenApi(x =>
        {
            x.Summary = "Delete multiple clients";
            x.Description = "Deletes multiple clients from a list of IDs sent in the request body.";
            return x;
        });

        return app;
    }
}