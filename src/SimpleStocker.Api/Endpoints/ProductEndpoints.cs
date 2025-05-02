using SimpleStocker.Api.Models.ViewModels;

namespace SimpleStocker.Api.Endpoints
{
    public static class ProductEndpoints
    {
        public static WebApplication MapProductEndpoints(this WebApplication service)
        {
            string basePath = "products";
            service.MapGet($"{basePath}", () =>
            {
                return Results.Ok();

            });

            service.MapGet($"{basePath}/{{id:long}}", (long id) =>
            {
                return Results.Ok();

            });

            service.MapPost($"{basePath}", (ProductViewModel model) =>
            {
                return Results.Ok();

            });

            service.MapPut($"{basePath}/{{id:long}}", (long id, ProductViewModel model) =>
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
