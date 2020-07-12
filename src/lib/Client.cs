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

          Console.WriteLine("Socket connected to {0}",
            sender.RemoteEndPoint.ToString());

          // Encode the data string into a byte array.
          byte[] msg = Encoding.ASCII.GetBytes(userInput + "<EOF>");

          // Send the data through the socket.
          int bytesSent = sender.Send(msg);

          // Receive the response from the remote device.
          int bytesRec = sender.Receive(bytes);
          Console.WriteLine("Echoed test = {0}",
            Encoding.ASCII.GetString(bytes, 0, bytesRec));

          // Release the socket.
          sender.Shutdown(SocketShutdown.Both);
          sender.Close();

        }
        catch (ArgumentNullException ane)
        {
          Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
        }
        catch (SocketException se)
        {
          Console.WriteLine("SocketException : {0}", se.ToString());
        }
        catch (Exception e)
        {
          Console.WriteLine("Unexpected exception : {0}", e.ToString());
        }

      }
      catch (Exception e)
      {
        Console.WriteLine(e.ToString());
      }
    }

    /// <summary>
    /// A method for getting user input from the console.
    /// </summary>
    private string GetUserInput()
    {
      System.Console.WriteLine("Waiting for input: ");
      return System.Console.ReadLine();
    }
  }
}