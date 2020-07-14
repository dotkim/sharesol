using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Sharesol
{
  /// <summary>
  /// Class used for writing to the console and logging verbosity
  /// </summary>
  public class Logger
  {
    private ConfigurationLoader Loader = new ConfigurationLoader();
    private Configuration Configuration { get; set; }

    /// <summary>
    /// An integer level for the logging.
    /// 1 = INFO (DEFAULT)
    /// 2 = VERBOSE
    /// 3 = ERROR
    /// 4 = DEBUG
    /// </summary>
    private int Verbosity { get; set; }

    private Dictionary<int, string> LevelTranslations = new Dictionary<int, string>
    {
      { 1, "INFO" },
      { 2, "WARN" },
      { 3, "ERROR" },
      { 4, "DEBUG" }
    };

    private Dictionary<string, LoggingLevel> Levels = new Dictionary<string, LoggingLevel>
      {
        { "INFO", new LoggingLevel { Verbosity = 1, Color = ConsoleColor.White } },
        { "WARN", new LoggingLevel { Verbosity = 2, Color = ConsoleColor.Yellow } },
        { "ERROR", new LoggingLevel { Verbosity = 3, Color = ConsoleColor.Red } },
        { "DEBUG", new LoggingLevel { Verbosity = 4, Color = ConsoleColor.Gray } }
      };

    /// <summary>
    /// Creates a new logger instance, using the default verbosity
    /// </summary>
    public Logger()
    {
      try
      {
        Configuration = Loader.LoadConfig();
        Verbosity = Levels[Configuration.Verbosity].Verbosity;
      }
      catch (Exception err)
      {
        Console.WriteLine(err.ToString());
      }
    }

    /// <summary>
    /// Creates a new logger instance, this takes the verbosity level
    /// </summary>
    /// <param name="verbosity">String representing the wanted verbosity level</param>
    public Logger(string verbosity)
    {
      try
      {
        Configuration = Loader.LoadConfig();
        if (string.IsNullOrEmpty(verbosity))
        {
          verbosity = Configuration.Verbosity;
        }

        LoggingLevel level;
        if (Levels.TryGetValue(verbosity, out level))
        {
          Verbosity = level.Verbosity;
        }
        else
        {
          Verbosity = Levels[Configuration.Verbosity].Verbosity;
          WriteLine("Verbosity level not recognized, using the default", 1);
        }
      }
      catch (Exception err)
      {
        Console.WriteLine(err.ToString());
      }
    }

    /// <summary>
    /// Writes a new line to the console
    /// </summary>
    /// <param name="message">The message to write as a string</param>
    /// <param name="severity">
    /// severity level: 1 = INFO (DEFAULT), 2 = VERBOSE, 3 = ERROR, 4 = DEBUG
    /// </param>
    /// <value>1</value>
    /// <param name="data">A boolean to write the line without clutter on received data</param>
    /// <value>false</value>
    public void WriteLine(string message, int severity = 1, Boolean data = false)
    {
      if (severity > Verbosity) return;

      string prefix = LevelTranslations[severity];
      LoggingLevel level = Levels[prefix];

      // Foreground color causes a wierd bug when running in the Linux terminal (Terminator)
      // I have disabled it for linux just because i cant be arsed to fix it
      // It has something to do with the change in color calling a refresh or whatever
      // Check it here: https://github.com/dotnet/runtime/issues/23241
      // and https://github.com/dotnet/runtime/issues/26138
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
      {
        Console.ForegroundColor = level.Color;
      }

      if (data)
      {
        Console.WriteLine(message);
      }
      else
      {
        Console.WriteLine($"[{prefix}] - {message}");
      }

      // Same as above
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
      {
        Console.ResetColor();
      }
    }
  }
}