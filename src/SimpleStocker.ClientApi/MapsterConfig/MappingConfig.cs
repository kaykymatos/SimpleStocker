using Mapster;

namespace SimpleStocker.ClientApi.MapsterConfig
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
