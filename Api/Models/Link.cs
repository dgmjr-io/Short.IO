namespace Short.IO.Api.Models;

public record class Link : LinkBase
{
    [JProp("idString")]
    public virtual string Id { get; set; }

    [JProp("OwnerId")]
    public virtual int OwnerId { get; set; }

    [JProp("cloaking")]
    public virtual bool IsCloakingEnabled { get; set; }
}
