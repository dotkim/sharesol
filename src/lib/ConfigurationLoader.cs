using System;
using System.IO;

namespace Sharesol
{
  /// <summary>
  /// Internal class for loading config from a file to the application.
  /// </summary>
  internal class ConfigurationLoader
  {
    static private string defaultConfigPath = Directory.GetCurrentDirectory() + @"/../Configuration.xml";
    private string cfgPath;

    private Configuration config = new Configuration();

    /// <summary>
    /// Self reffering constructor if no path is provided on a new instance
    /// </summary>
    public ConfigurationLoader() : this(defaultConfigPath) { }

    /// <summary>
    /// Creates a new instance of the Loader
    /// </summary>
    /// <param name="cfgPath">Takes a path to the config as a string</param>
    public ConfigurationLoader(string cfgPath)
    {
      this.cfgPath = cfgPath;
    }

    /// <summary>
    /// Loads the config from the configuration file
    /// </summary>
    /// <returns>Returns a <see cref="Configuration"/> object</returns>
    public Configuration LoadConfig()
    {
      try
      {
        if (!File.Exists(cfgPath))
        {
          throw new Exception("File not found.");
        }

        string cfgContent = File.ReadAllText(cfgPath);
        config = Xml.Deserialize<Configuration>(cfgContent);

        return config;
      }
      catch (Exception err)
      {
        throw err;
      }
    }
  }
}