namespace NShadow.Core.Options;

public sealed class ShadowOptions
{
    public string Password { get; set; } = null!;

    public string Method { get; set; } = null!;

    public TimeSpan Timeout { get; set; }
}