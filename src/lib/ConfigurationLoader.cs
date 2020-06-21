using System;
using System.IO;
using System.Text.Json;

namespace Sharesol
{
  /// <summary>
  /// Internal class for loading config from a file to the application.
  /// </summary>
  internal class ConfigurationLoader
  {
    static private string defaultConfigPath = Directory.GetCurrentDirectory() + @"/appsettings.json";

    private Configuration config = new Configuration();
    private string cfgPath;

    public ConfigurationLoader() : this(defaultConfigPath) { }
    public ConfigurationLoader(string cfgPath)
    {
      this.cfgPath = cfgPath;
    }

    /// <summary>
    /// Loads the config from the appsettings.json file.
    /// Returns the <see cref="Configuration"/> type.
    /// </summary>
    public Configuration LoadConfig()
    {
      try
      {
        if (!File.Exists(cfgPath))
        {
          throw new Exception("Missing the appsettings.json file, stopping application.");
        }

        string cfgContent = File.ReadAllText(cfgPath);

        config = Deserialize(cfgContent);

        return config;
      }
      catch (Exception)
      {
        throw;
      }
    }

    /// <summary>
    /// Used for deserializing json, only in use for parsing the config file.
    /// </summary>
    private Configuration Deserialize(string cfgContent)
    {
      return JsonSerializer.Deserialize<Configuration>(cfgContent);
    }
  }
}