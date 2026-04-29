namespace NShadow.Core.Options;

public sealed class ShadowOptions
{
    public int Port { get; set; }

    public string Password { get; set; } = null!;

    public string Method { get; set; } = null!;

    public TimeSpan Timeout { get; set; }
}