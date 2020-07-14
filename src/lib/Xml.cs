using System.IO;
using System.Xml.Serialization;

namespace Sharesol
{
  /// <summary>
  /// Class used for processing XML data
  /// </summary>
  public static class Xml
  {
    /// <summary>
    /// Deserializes XML data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static T Deserialize<T>(string content) where T : class
    {
      var serializer = new XmlSerializer(typeof(T));
      using (var reader = new StringReader(content))
        return (T)serializer.Deserialize(reader);
    }
  }
}