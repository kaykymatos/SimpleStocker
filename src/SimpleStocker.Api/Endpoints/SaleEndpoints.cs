using SimpleStocker.Api.Models.ViewModels;
using SimpleStocker.Api.Services;

namespace SimpleStocker.Api.Endpoints
{
    public static class SaleEndpoints
    {
        public static WebApplication MapSaleEndpoints(this WebApplication app)
        {
            return app.MapCrudEndpoints<ISaleService, SaleViewModel>("sales");
        }
    }
}
