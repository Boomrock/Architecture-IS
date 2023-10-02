using NetController;
using NetProtocol;
using System.Net;

namespace ConsoleClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IClient<Command> client = new  Client(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8808));
            client.Send(new Command { CommandType = CommandType.Add, Data = null });
            Console.WriteLine("отправленно");
            Console.ReadLine();

        }
    }
}