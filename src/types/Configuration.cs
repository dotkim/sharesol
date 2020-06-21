namespace Sharesol
{
  /// <summary>Type class to contain the application config.</summary>
  public class Configuration
  {
    /// <summary>An integer port for the remote server, this is used by the client.</summary>
    public int RemotePort { get; set; }
    /// <summary>A Remote IP as a string, this is used by the client.</summary>
    public string RemoteIP { get; set; }
  }
}