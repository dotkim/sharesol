using System.Threading.Tasks;
using Sharesol.Client;
using Sharesol.Server;

namespace Sharesol
{
  class Program
  {
    static void Main(string[] args)
    {
      var server = new SynchronousSocketListener();
      Task SocketListener = new Task(server.StartListening);

      SocketListener.Start();
      while (true)
      {
        var client = new SynchronousSocketClient();
        Task SocketClient = new Task(client.StartClient);
        SocketClient.RunSynchronously();
      }
    }
  }
}