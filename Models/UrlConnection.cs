namespace HavenoSharp.Models;

public enum OnlineStatus
{
    UNKNOWN = 0,
    ONLINE = 1,
    OFFLINE = 2
}

public enum AuthenticationStatus
{
    NO_AUTHENTICATION = 0,
    AUTHENTICATED = 1,
    NOT_AUTHENTICATED = 2
}

public class UrlConnection
{
    public string Url { get; set; } = string.Empty;
    public OnlineStatus OnlineStatus { get; set; }
    public AuthenticationStatus AuthenticationStatus { get; set; }
}
