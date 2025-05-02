using SimpleStocker.Api.Models.ViewModels;

namespace SimpleStocker.Api.Endpoints
{
    public static class CategoryEndpoints
    {
        public static WebApplication MapCategoryEndpoints(this WebApplication service)
        {
            string basePath = "categories";
            service.MapGet($"{basePath}", () =>
            {
                return Results.Ok();

            });

            service.MapGet($"{basePath}/{{id:long}}", (long id) =>
            {
                return Results.Ok();

            });

            service.MapPost($"{basePath}", (CategoryViewModel model) =>
            {
                return Results.Ok();

            });

            service.MapPut($"{basePath}/{{id:long}}", (long id, CategoryViewModel model) =>
            {
                return Results.Ok();
            });

            service.MapDelete($"{basePath}/{{id:long}}", (long id) =>
            {
                return Results.Ok();

            });

            return service;

        }
    }
}
