using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Sharesol.Server
{
  /// <summary>
  /// A synchronous server, this listens to the local endpoint for a message.
  /// </summary>
  public class SynchronousSocketListener
  {
    private ConfigurationLoader Loader = new ConfigurationLoader();
    private Logger Log = new Logger();
    /// <summary>
    /// Incoming data from the client.  
    /// </summary>
    public static string data = null;

    /// <summary>
    /// A method to begin listening on the endpoint.
    /// This will build the localendpoint through hostname.
    /// </summary>
    public void StartListening()
    {
      Configuration config = Loader.LoadConfig();

      // Data buffer for incoming data.  
      byte[] bytes = new Byte[1024];

      // Establish the local endpoint for the socket.  
      // Dns.GetHostName returns the name of the
      // host running the application.  
      IPAddress ipAddress = IPAddress.Parse(config.LocalIP);
      IPEndPoint localEndPoint = new IPEndPoint(ipAddress, config.LocalPort);
      Log.WriteLine($"Local endpoint created at: {ipAddress}:{config.LocalPort}", 1);

      // Create a TCP/IP socket.  
      Socket listener = new Socket(ipAddress.AddressFamily,
          SocketType.Stream, ProtocolType.Tcp);

      // Bind the socket to the local endpoint and
      // listen for incoming connections.  
      try
      {
        listener.Bind(localEndPoint);
        listener.Listen(10);

        // Start listening for connections.  
        while (true)
        {
          Log.WriteLine("Waiting for a connection...", 4);
          // Program is suspended while waiting for an incoming connection.  
          Socket handler = listener.Accept();
          data = null;

          // An incoming connection needs to be processed.  
          while (true)
          {
            int bytesRec = handler.Receive(bytes);
            data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
            if (data.IndexOf("<EOF>") > -1)
            {
              break;
            }
          }

          data = data.Substring(0, data.IndexOf("<EOF>"));

          // Show the data on the console.
          Log.WriteLine($"Text received: {data}", 4);
          Log.WriteLine(data, 1, true);

          // Echo the data back to the client.  
          byte[] msg = Encoding.ASCII.GetBytes(data);

          handler.Send(msg);
          handler.Shutdown(SocketShutdown.Both);
          handler.Close();
        }

      }
      catch (Exception e)
      {
        Log.WriteLine(e.ToString(), 3);
      }

      Console.WriteLine("\nPress ENTER to continue...");
      Console.Read();

    }
  }
}