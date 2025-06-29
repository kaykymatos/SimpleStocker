using Mapster;

namespace SimpleStocker.InventoryApi.MapsterConfig
{
    public static class MappingConfig
    {
        public static IServiceCollection RegisterMapster(this IServiceCollection services)
        {
            services.AddMapster();
            return services;
        }
    }
}
