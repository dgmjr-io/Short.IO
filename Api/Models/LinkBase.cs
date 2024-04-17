namespace Short.IO.Api.Models;

public record class LinkBase()
{
    /// <summary>
    /// Allow duplicate links. If true, you can create multiple links with the same original URL. False by default
    /// </summary>
    [JProp("allowDuplicates")]
    public virtual bool AllowDuplicates { get; set; } = false;

    [JProp("path")]
    public virtual string Path { get; set; }

    [JProp("originalURL")]
    [Required]
    public virtual Uri Url { get; set; }

    [JProp("title")]
    public virtual string Title { get; set; }

    [JProp("tags")]
    public virtual string[] Tags { get; set; }

    [JProp("expiresAt")]
    [JsonConverter<JsonUnixTimeMillisecondsConverter>]
    public virtual datetime? ExpiresAt { get; set; }

    [JProp("expiredURL")]
    public virtual Uri? ExpiredUrl { get; set; }

    [JProp("redirectType")]
    [JsonConverter<JsonNumberEnumConverter<RedirectType>>]
    public virtual RedirectType RedirectType { get; set; } = RedirectType.TemporaryRedirect;
}
