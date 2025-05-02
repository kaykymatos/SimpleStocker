using SimpleStocker.Api.Models.ViewModels;

namespace SimpleStocker.Api.Endpoints
{
    public static class ClientEndpoints
    {
        public static WebApplication MapClientEndpoints(this WebApplication service)
        {
            string basePath = "clients";
            service.MapGet($"{basePath}", () =>
            {
                return Results.Ok();

            });

            service.MapGet($"{basePath}/{{id:long}}", (long id) =>
            {
                return Results.Ok();

            });

            service.MapPost($"{basePath}", (ClientViewModel model) =>
            {
                return Results.Ok();

            });

            service.MapPut($"{basePath}/{{id:long}}", (long id, ClientViewModel model) =>
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
