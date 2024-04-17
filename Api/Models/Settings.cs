namespace Short.IO.Api.Models;

public class Settings
{
    public virtual Uri BaseUrl { get; set; } = new("https://api.short.io/api");
    public virtual string DefaultDomain { get; set; }
    public virtual string ApiKey { get; set; }
    public virtual string[] DefaultTags { get; set; }
    public virtual duration DefaultTimeToLive { get; set; } = duration.FromDays(30);
    public virtual RedirectType DefaultRedirectType { get; set; } = RedirectType.TemporaryRedirect;
    public virtual string[] LinkCreators { get; set; } = [];
    public virtual string[] LinkAdministrators { get; set; } = [];
}
