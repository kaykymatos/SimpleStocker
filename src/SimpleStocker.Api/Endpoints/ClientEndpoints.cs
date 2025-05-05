using SimpleStocker.Api.Models.ViewModels;
using SimpleStocker.Api.Services;

namespace SimpleStocker.Api.Endpoints
{
    public static class ClientEndpoints
    {
        public static WebApplication MapClientEndpoints(this WebApplication app)
        {
            return app.MapCrudEndpoints<IClientService, ClientViewModel>("clients");
        }
    }
}
