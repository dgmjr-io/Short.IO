namespace Short.IO.Bot.Configure;

using Telegram.Bot.Components;

public class BotComponents : ConfiguratorBase<BotComponents>
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<TelegramBotComponent>();
        // services.AddSingleton<CustomFunctionsComponent>();
        // services.AddSingleton<AzureTableStorageComponent>();
    }
}
