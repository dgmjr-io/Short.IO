namespace Short.IO.Api.Models;

public enum RedirectType
{
    Permanent = 301,
    Temporary = 302,
    SeeOther = 303,
    TemporaryRedirect = 307,
    PermanentRedirect = 308
}
