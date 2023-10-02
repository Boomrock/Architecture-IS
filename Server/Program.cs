using NetController;
using NetProtocol;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IServer<Command, CommandType> server = new NetController.Server();
            server.AddRoute(CommandType.Add, (command) =>
            {
                return MessageBuilder.BuildMessage("You reqeust me", MessageType.Message);
            });
            server.Start();
            Console.ReadLine();

        }
    }
}