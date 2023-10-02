using System;
using System.Net.Sockets;

namespace NetController
{
    internal class Server<TMessage, CommandType> : IServer<TMessage ,CommandType> where CommandType: Enum
    {
        public readonly Dictionary<Enum, Action<TMessage>> Routes;
        private readonly IReceiver<TMessage> _receiver;
        private readonly ISender<TMessage> _sender;
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

        private void MessageReceivedEventHandler(System.Net.IPEndPoint arg1, int arg2, TMessage arg3)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            _receiver.Stop();

        }

        public void AddRoute(CommandType key, Action<TMessage> Handler)
        {
            Routes.Add(key, Handler);
        }
    }
}
