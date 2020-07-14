namespace Sharesol
{
  /// <summary>Type class to contain the application config.</summary>
  public class Configuration
  {
    /// <summary>An integer port for the remote server, this is used by the client.</summary>
    /// <value>11000</value>
    public int RemotePort { get; set; } = 11000;

    /// <summary>A Remote IP as a string, this is used by the client.</summary>
    /// <value>127.0.0.1</value>
    public string RemoteIP { get; set; } = "127.0.0.1";

    /// <summary>An integer port for the local server, this is used by the remote client.</summary>
    /// <value>11000</value>
    public int LocalPort { get; set; } = 11000;

    /// <summary>The local server IP as a string.</summary>
    /// <value>127.0.0.1</value>
    public string LocalIP { get; set; } = "127.0.0.1";

    /// <summary>Verbosity level, can be overridden with CLI argument.</summary>
    /// <value>INFO</value>
    public string Verbosity { get; set; } = "INFO";
  }
}