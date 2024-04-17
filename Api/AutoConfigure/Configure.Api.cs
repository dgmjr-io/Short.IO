namespace Short.IO.Api.Configure;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public class Api : ConfiguratorBase<Api>
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);
        services.AddShortIOApi(Configuration);
    }
}
