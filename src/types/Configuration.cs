namespace Sharesol
{
  /// <summary>Type class to contain the application config.</summary>
  public class Configuration
  {
    /// <summary>An integer port for the remote server, this is used by the client.</summary>
    public int RemotePort { get; set; } = 11000;
    /// <summary>A Remote IP as a string, this is used by the client.</summary>
    public string RemoteIP { get; set; } = "127.0.0.1";
    /// <summary>An integer port for the local server, this is used by the remote client.</summary>
    public int LocalPort { get; set; } = 11000;
    /// <summary>The local server IP as a string.</summary>
    public string LocalIP { get; set; } = "127.0.0.1";
  }
}