using System;
using System.Collections.Generic;

namespace Sharesol
{
  /// <summary>
  /// Class used for writing to the console and logging verbosity.
  /// </summary>
  public class Logging
  {
    /// <summary>
    /// An integer level for the logging.
    /// 1 = INFO (DEFAULT)
    /// 2 = ERROR
    /// 3 = VERBOSE
    /// 4 = DEBUG
    /// </summary>
    private int Verbosity { get; set; } = 1;
    private string Prefix {get;set;} = "[INFO] - ";

    /// <summary>
    /// Creates a new logger instance with default verbosity.
    /// </summary>
    public Logging() { }

    /// <summary>
    /// Creates a new logger instance, this takes the verbosity level.
    /// </summary>
    public Logging(string verbosity)
    {
      Dictionary<string, int> levels = new Dictionary<string, int>
      {
        { "INFO", 1 },
        { "ERROR", 2 },
        { "VERBOSE", 3 },
        { "DEBUG", 4 }
      };

      int value;
      if (levels.TryGetValue(verbosity, out value))
      {
        Verbosity = value;
        Prefix = $"[{verbosity}] - ";
      }
      else {
        System.Console.WriteLine("[WARN] - Verbosity level not recognized, using the default.");
      }
    }
  }
}