namespace Short.IO.Bot.Configure;

using System.Text.Json;

using Azure.Core.Serialization;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Rest.Serialization;

using Expression = AdaptiveExpressions.Expression;
using IMiddleware = Microsoft.Bot.Builder.IMiddleware;

public class Mvc : ConfiguratorBase<Mvc>
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services
            .AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.MaxDepth = HttpHelper
                    .BotMessageSerializerSettings
                    .MaxDepth;
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.MaxDepth =
                    HttpHelper.BotMessageSerializerSettings.MaxDepth ?? int.MaxValue;
                options.JsonSerializerOptions.NumberHandling = JNumbers.AllowReadingFromString;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = JNaming.SnakeCaseLower;
                options.JsonSerializerOptions.DictionaryKeyPolicy = JNaming.SnakeCaseLower;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JIgnore.WhenWritingNull;
                options.JsonSerializerOptions.AllowTrailingCommas = true;
                options.JsonSerializerOptions.ReadCommentHandling = JComments.Skip;
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.Converters.Add(
                    new SystemTextJsonNewtonsoftJsonWrapperConverter<
                        AdaptiveExpressions.Expression,
                        ExpressionConverter
                    >()
                );
                options.JsonSerializerOptions.Converters.Add(
                    new System.Text.Json.Iso8601TimeSpanConverter()
                );
            });
        services.AddDgmjrBotRuntime(Configuration);
    }

    protected override void Configure(IApplicationBuilder app)
    {
        app.UseDefaultFiles();

        // Set up custom content types - associating file extension to MIME type.
        var provider = new FileExtensionContentTypeProvider();
        provider.Mappings[".lu"] = "application/vnd.microsoft.lu";
        provider.Mappings[".qna"] = "application/vnd.microsoft.qna";

        // Expose static files in manifests folder for skill scenarios.
        app.UseHttpsRedirection();
        app.UseStaticFiles(new StaticFileOptions { ContentTypeProvider = provider });
        app.UseWebSockets();
        app.UseRouting();
        app.UseAuthorization();
        app.UseStatusCodePages();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        app.UseBotFramework();
    }
}
