using System;

namespace Sharesol
{
  /// <summary>
  /// Represents a logginglevel, used for storing related data
  /// </summary>
  public class LoggingLevel
  {
    /// <summary>
    /// Verbosity level
    /// </summary>
    public int Verbosity { get; set; }

    /// <summary>
    /// Text color for the message
    /// </summary>
    public ConsoleColor Color { get; set; }
  }
}