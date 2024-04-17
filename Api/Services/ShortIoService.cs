namespace Short.IO.Api.Services;

using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;

using Short.IO.Api.Models;

public class ShortIOService(
    IOptions<Settings> settings,
    ILogger<ShortIOService> logger,
    IHttpClientFactory clientFactory,
    IOptions<JsonOptions> jsonOptions
) : IShortIOService
{
    private readonly Jso Jso = new(jsonOptions.Value.JsonSerializerOptions);

    private HttpClient NewClient() => clientFactory.CreateClient();

    protected virtual Task<HttpResponseMessage> GetAsync(string path)
    {
        var client = NewClient();
        return client.GetAsync(path);
    }

    protected virtual async Task<TResponse?> PostAsync<TRequest, TResponse>(
        string path,
        TRequest postBody
    )
    {
        using var client = NewClient();
        var response = await client.PostAsJsonAsync(path, postBody, Jso);
        return await response.Content.ReadFromJsonAsync<TResponse>(Jso);
    }

    [HttpPost(Constants.Routes.Links)]
    public virtual async Task<Link?> CreateLinkAsync(Uri url, string? domain = default)
    {
        var request = new CreateLinkRequest
        {
            Url = url,
            Domain = domain ?? settings.Value.DefaultDomain,
            TimeToLive = settings.Value.DefaultTimeToLive,
            RedirectType = settings.Value.DefaultRedirectType,
            Tags = settings.Value.DefaultTags,
        };
        return await PostAsync<CreateLinkRequest, Link>("links", request);
    }
}
