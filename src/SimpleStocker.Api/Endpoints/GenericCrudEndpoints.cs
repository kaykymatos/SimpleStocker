using Microsoft.AspNetCore.Mvc;
using SimpleStocker.Api.Models.ViewModels;
using SimpleStocker.Api.Services;

namespace SimpleStocker.Api.Endpoints
{
    public static class GenericCrudEndpoints
    {
        public static WebApplication MapCrudEndpoints<TService, TViewModel>(
            this WebApplication app,
            string basePath)
            where TService : class, IBaseService<TViewModel>
            where TViewModel : BaseViewModel
        {
            app.MapGet($"{basePath}", async ([FromServices] TService service) =>
            {
                var response = await service.GetAllAsync();
                return response.Success ? Results.Ok(response) : Results.BadRequest(response);
            });

            app.MapGet($"{basePath}/{{id:long}}", async ([FromServices] TService service, [FromRoute] long id) =>
            {
                var response = await service.GetOneAsync(id);
                return response.Success ? Results.Ok(response) : Results.BadRequest(response);
            });

            app.MapPost($"{basePath}", async ([FromServices] TService service, [FromBody] TViewModel model) =>
            {
                var response = await service.CreateAsync(model);
                return response.Success ? Results.Ok(response) : Results.BadRequest(response);
            });

            app.MapPut($"{basePath}/{{id:long}}", async ([FromServices] TService service, [FromRoute] long id, [FromBody] TViewModel model) =>
            {
                model.Id = id; // Garante que o ID está sendo passado
                var response = await service.UpdateAsync(model);
                return response.Success ? Results.Ok(response) : Results.BadRequest(response);
            });

            app.MapDelete($"{basePath}/{{id:long}}", async ([FromServices] TService service, [FromRoute] long id) =>
            {
                var response = await service.DeleteAsync(id);
                return response.Success ? Results.Ok(response) : Results.BadRequest(response);
            });

            return app;
        }
    }
}
