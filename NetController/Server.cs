using NetProtocol;
using System;
using System.Net;
using System.Net.Sockets;

namespace NetController
{
    internal class Server : IServer<Command , CommandType>
    {
        public readonly Dictionary<CommandType, Func<Command, Message>> Routes;
        private readonly IReceiver<Command> _receiver;
        private readonly ISender<Message> _sender;
        private readonly UdpClient _udpClient;
        public Server() { 
            _udpClient = new UdpClient(8808);
            Routes = new();
            
        }

        public bool Start()
        {
            _receiver.Start();
            _receiver.OnReceive += MessageReceivedEventHandler;
            return true;
        }

        private void MessageReceivedEventHandler(IPEndPoint endPoint, Command command)
        {
            Message message;
            try
            {
                if (Routes.TryGetValue(command.CommandType, out var handler))
                {
                    message = handler.Invoke(command);
                }
                else
                {
                    message = MessageBuilder
                        .BuildException(
                        $"dictionori doesn't contains commandHandler for {command.CommandType} \n");
                }
            }
            catch (Exception ex)
            {
                message =  MessageBuilder.BuildException(ex);
            }
            _sender.Send(endPoint, message);
        }

        public void Stop()
        {
            _receiver.Stop();
        }

        public void AddRoute(CommandType key, Func<Command, Message> Handler)
        {
            Routes.Add(key, Handler);
        }
    }
}
