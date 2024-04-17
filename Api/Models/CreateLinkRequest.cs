namespace Short.IO.Api.Models;

public record class CreateLinkRequest : LinkBase
{
    [JProp("domain")]
    [Required]
    public virtual string Domain { get; set; }

    [JProp("cloaking")]
    public virtual bool EnableCloaking { get; set; } = false;

    [JProp("ttl")]
    [JsonConverter<JsonIntegerToTimeSpanConverter>]
    public virtual duration TimeToLive { get; set; } = duration.FromDays(30);
}
