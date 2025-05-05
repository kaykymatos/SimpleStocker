using SimpleStocker.Api.Models.ViewModels;
using SimpleStocker.Api.Services;

namespace SimpleStocker.Api.Endpoints
{
    public static class CategoryEndpoints
    {
        public static WebApplication MapCategoryEndpoints(this WebApplication app)
        {
            return app.MapCrudEndpoints<ICategoryService, CategoryViewModel>("categories");
        }
    }
}
