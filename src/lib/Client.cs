using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Sharesol.Client
{
  /// <summary>
  /// A Synchronous client, this will connect to a remote or local socket server.
  /// Server information should be provided in the appsettings file.
  /// </summary>
  public class SynchronousSocketClient
  {
    private ConfigurationLoader Loader = new ConfigurationLoader();
    private Logger Log = new Logger();

    /// <summary>
    /// Method StartClient will start the client process, does not return anything.
    /// Will reload config every loop.
    /// </summary>
    public void StartClient()
    {
      Configuration config = Loader.LoadConfig();

      // Data buffer for incoming data.
      byte[] bytes = new byte[1024];

      // Connect to a remote device.
      try
      {
        // Establish the remote endpoint for the socket.
        IPAddress ipAddress = IPAddress.Parse(config.RemoteIP);
        IPEndPoint remoteEP = new IPEndPoint(ipAddress, config.RemotePort);

        // Create a TCP/IP  socket.
        Socket sender = new Socket(ipAddress.AddressFamily,
          SocketType.Stream, ProtocolType.Tcp);

        // Connect the socket to the remote endpoint. Catch any errors.
        try
        {
          string userInput = GetUserInput();
          sender.Connect(remoteEP);

          Log.WriteLine($"Socket connected to {sender.RemoteEndPoint.ToString()}", 4);

          // Encode the data string into a byte array.
          byte[] msg = Encoding.ASCII.GetBytes(userInput + "<EOF>");

          // Send the data through the socket.
          int bytesSent = sender.Send(msg);

          // Receive the response from the remote device.
          int bytesRec = sender.Receive(bytes);
          Log.WriteLine($"Echoed {Encoding.ASCII.GetString(bytes, 0, bytesRec)}", 4);

          // Release the socket.
          sender.Shutdown(SocketShutdown.Both);
          sender.Close();

        }
        catch (ArgumentNullException ane)
        {
          Log.WriteLine($"ArgumentNullException : {ane.ToString()}", 3);
        }
        catch (SocketException se)
        {
          Log.WriteLine($"SocketException : {se.ToString()}", 3);
        }
        catch (Exception e)
        {
          Log.WriteLine($"Unexpected exception : {e.ToString()}", 3);
        }

      }
      catch (Exception e)
      {
        Log.WriteLine(e.ToString(), 3);
      }
    }

    /// <summary>
    /// A method for getting user input from the console.
    /// </summary>
    private string GetUserInput()
    {
      Log.WriteLine("Waiting for input:", 4);
      return System.Console.ReadLine();
    }
  }
}