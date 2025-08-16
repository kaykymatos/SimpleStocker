using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using SimpleStocker.ProductApi.DTO;
using SimpleStocker.ProductApi.Services;

namespace SimpleStocker.ProductApi.Endpoints
{
    public static class Categoryndpoints
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
                x.Summary = "Obtem um Category pelo ID";
                x.Description = "Retorna os dados de um Category específico a partir do ID informado na URL.";
                x.Parameters = new List<OpenApiParameter>
                            {
                                new OpenApiParameter
                                {
                                    Name = "id",
                                    In = ParameterLocation.Path,
                                    Required = true,
                                    Description = "ID do Category",
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
                x.Summary = "Deleta um Category pelo ID";
                x.Description = "Deleta um Category a partir do ID informado na URL.";
                x.Parameters = new List<OpenApiParameter>
                        {
                            new OpenApiParameter
                            {
                                Name = "id",
                                In = ParameterLocation.Path,
                                Required = true,
                                Description = "ID do Category",
                                Schema = new OpenApiSchema { Type = "integer", Format = "int64" }
                            }
                        };
                return x;
            });
            return app;
        }

    }
}
