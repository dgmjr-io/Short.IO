namespace Microsoft.Extensions.DependencyInjection;

public static class ShortIOApiServiceCollectionExtensions
{
    public static IServiceCollection AddShortIOApi(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSingleton<IShortIOService, ShortIOService>();
        services.ConfigureAll<Settings>(settings =>
        {
            configuration.GetSection($"{nameof(Short)}.{nameof(global::Short.IO)}").Bind(settings);
        });
        return services;
    }
}
