namespace HavenoSharp.Singletons;

public class GrpcSettingsSingleton
{
    public string Password { get; private set; } = string.Empty;
    public string Host { get; private set; } = string.Empty;

    public event Action<string, string>? SettingsChanged;

    public void UpdateSettings(string host, string password)
    {
        Host = host;
        Password = password;
        SettingsChanged?.Invoke(host, password);
    }

    public GrpcSettingsSingleton()
    {

    }
}
