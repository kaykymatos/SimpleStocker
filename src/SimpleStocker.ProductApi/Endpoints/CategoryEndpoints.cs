using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using SimpleStocker.ProductApi.DTO;
using SimpleStocker.ProductApi.Services;

namespace SimpleStocker.ProductApi.Endpoints
{
    public static class CategoryEndpoints
    {
        public static WebApplication MapCategoryEndpoints(this WebApplication app)
        {
            app.MapPost("categories", async ([FromBody] CategoryDTO model, [FromServices] ICategoryService service) =>
            {
                var response = await service.CreateAsync(model);
                return response.Success ? Results.Ok(response) : Results.BadRequest(response);

            }).WithOpenApi(x =>
            {
                x.Summary = "";
                x.Description = "";
                return x;
            });

            app.MapPut("categories/{id:long}", async ([FromRoute] long id, [FromBody] CategoryDTO model, [FromServices] ICategoryService service) =>
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

            app.MapGet("categories/{id:long}", async ([FromRoute] long id, [FromServices] ICategoryService service) =>
            {
                var response = await service.GetOneAsync(id);
                return response.Success ? Results.Ok(response) : Results.BadRequest(response);
            }).WithOpenApi(x =>
            {
                x.Summary = "Get a Category by ID";
                x.Description = "Returns the data of a specific Category based on the ID provided in the URL.";
                x.Parameters = new List<OpenApiParameter>
                            {
                                    new OpenApiParameter
                                    {
                                        Name = "id",
                                        In = ParameterLocation.Path,
                                        Required = true,
                                        Description = "Category ID",
                                        Schema = new OpenApiSchema { Type = "integer", Format = "int64" }
                                    }
                            };
                return x;
            });

            app.MapGet("categories", async ([FromServices] ICategoryService service) =>
            {
                var response = await service.GetAllAsync();
                return response.Success ? Results.Ok(response) : Results.BadRequest(response);
            }).WithOpenApi(x =>
            {
                x.Summary = "";
                x.Description = "";
                return x;
            });

            app.MapDelete("categories/{id:long}", async ([FromRoute] long id, [FromServices] ICategoryService service) =>
            {
                var response = await service.DeleteAsync(id);
                return response.Success ? Results.Ok(response) : Results.BadRequest(response);
            }).WithOpenApi(x =>
            {
                x.Summary = "Delete a Category by ID";
                x.Description = "Deletes a Category based on the ID provided in the URL.";
                x.Parameters = new List<OpenApiParameter>
                        {
                                new OpenApiParameter
                                {
                                    Name = "id",
                                    In = ParameterLocation.Path,
                                    Required = true,
                                    Description = "Category ID",
                                    Schema = new OpenApiSchema { Type = "integer", Format = "int64" }
                                }
                        };
                return x;
            });

            app.MapDelete("categories/batch", async ([FromBody] List<long> ids, [FromServices] ICategoryService service) =>
            {
                var response = await service.DeleteManyAsync(ids);
                return response.Success ? Results.Ok(response) : Results.BadRequest(response);
            }).WithOpenApi(x =>
            {
                x.Summary = "Delete multiple categories";
                x.Description = "Deletes multiple categories based on a list of IDs sent in the request body.";
                return x;
            });
            return app;
        }

    }
}
