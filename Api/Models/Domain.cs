namespace Short.IO.Api.Models;

public record class Domain
{
    public virtual int Id { get; set; }
    public virtual int? TeamId { get; set; }
    public virtual string Hostname { get; set; }
}
