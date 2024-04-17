namespace Short.IO.Bot.Configure;

using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Azure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Logging.Abstractions;
using Serilog;
using Serilog.Extensions.Logging;
using ClaimsIdentity = System.Security.Claims.ClaimsIdentity;
using ILogger = Microsoft.Extensions.Logging.ILogger;

public abstract class ConfiguratorBase<TConfigurator> : IHostingStartup, ILog, IStartupFilter
    where TConfigurator : ConfiguratorBase<TConfigurator>
{
    private readonly ILogger _nullLogger = new NullLogger<TConfigurator>();
    private ILogger _logger;
    public virtual ILogger Logger =>
        _logger ??= StaticLogger.GetLogger<TConfigurator>() ?? _nullLogger;

    public IConfiguration Configuration { get; private set; }
    public IWebHostEnvironment Environment { get; private set; }

    protected virtual void ConfigureAppConfiguration(
        WebHostBuilderContext context,
        IConfigurationBuilder config
    ) { }

    protected virtual void ConfigureServices(IServiceCollection services) { }

    protected virtual void Configure(IApplicationBuilder app) { }

    public void Configure(IWebHostBuilder builder)
    {
        builder
            .ConfigureServices(
                (context, services) =>
                {
                    Configuration = context.Configuration;
                    Environment = context.HostingEnvironment;
                    services.AddSingleton<IStartupFilter>(this);
                    ConfigureServices(services);
                }
            )
            .ConfigureAppConfiguration(ConfigureAppConfiguration);
    }

    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next) =>
        app =>
        {
            Configure(app);
            next(app);
        };
}
