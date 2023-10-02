using NetProtocol;
using System;
using System.Net.Sockets;

namespace NetController
{
    internal class Server : IServer<Command , CommandType>
    {
        public readonly Dictionary<CommandType, Func<Command, string>> Routes;
        private readonly IReceiver<Command> _receiver;
        private readonly ISender<Command> _sender;
        private readonly UdpClient _udpClient;
        public Server() { 
            _udpClient = new UdpClient(8808);
            Routes = new();
            
        }

        public bool Start(Action RequestHandler)
        {
            _receiver.Start(RequestHandler);
            _receiver.MessageReceivedEvent += MessageReceivedEventHandler;
            return true;
        }



        public void Stop()
        {
            _receiver.Stop();

        }

        public void AddRoute(CommandType key, Func<Command, string> Handler)
        {
            Routes.Add(key, Handler);
        }
    }
}
