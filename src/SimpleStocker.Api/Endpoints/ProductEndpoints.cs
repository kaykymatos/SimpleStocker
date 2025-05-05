using SimpleStocker.Api.Models.ViewModels;
using SimpleStocker.Api.Services;

namespace SimpleStocker.Api.Endpoints
{
    public static class ProductEndpoints
    {
        public static WebApplication MapProductEndpoints(this WebApplication app)
        {
            return app.MapCrudEndpoints<IProductService, ProductViewModel>("products");
        }
    }
}
