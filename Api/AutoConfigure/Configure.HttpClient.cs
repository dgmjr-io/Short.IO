namespace Short.IO.Api.Configure;

using Dgmjr.BotFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Short.IO.Api.Services;
using static System.IO.Path;

public class HttpClient : ConfiguratorBase<HttpClient>
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);
        services
            .AddHttpClient<IShortIOService>()
            .ConfigureHttpClient(
                (services, client) =>
                {
                    var settings = services.GetRequiredService<IOptions<Settings>>();
                    client.DefaultRequestHeaders.Add("Authorization", settings.Value.ApiKey);
                    client.BaseAddress = settings.Value.BaseUrl;
                }
            );
    }
}
