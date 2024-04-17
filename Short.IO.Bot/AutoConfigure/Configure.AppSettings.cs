namespace Short.IO.Bot.Configure;

using Dgmjr.BotFramework;

using ConfigurationBuilderExtensions = Microsoft.Bot.Builder.Dialogs.Adaptive.Runtime.Extensions.ConfigurationBuilderExtensions;

using Serilog;
using Serilog.Extensions.Logging;
using Serilog.Settings.Configuration;

using static System.IO.Path;

public class AppSettings : ConfiguratorBase<AppSettings>
{
    protected override void ConfigureAppConfiguration(
        WebHostBuilderContext context,
        IConfigurationBuilder config
    )
    {
        Console.WriteLine(
            $"{nameof(Configure)}.{nameof(AppSettings)}: {context.HostingEnvironment.EnvironmentName}"
        );
        var applicationRoot = CurrentDomain.BaseDirectory;
        var environmentName = context.HostingEnvironment.EnvironmentName;
        var settingsDirectory = "settings";

        config.AddUserSecrets<AppSettings>();

        config.AddKeyPerJsonFile(
            Join(GetDirectoryName(typeof(AppSettings).Assembly.Location), settingsDirectory),
            settingsDirectory,
            true,
            false
        );

        ConfigurationBuilderExtensions.AddBotRuntimeConfiguration(
            config,
            applicationRoot,
            settingsDirectory,
            environmentName
        );

        Serilog.Log.Logger = new LoggerConfiguration().ReadFrom
            .Configuration(
                (config as IConfigurationManager)!,
                new ConfigurationReaderOptions { SectionName = nameof(Serilog) }
            )
            .CreateBootstrapLogger();
    }
}
