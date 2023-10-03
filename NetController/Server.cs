using NetProtocol;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Sockets;

namespace NetController
{
    public class Server : IServer<Command , CommandType>
    {
        public readonly Dictionary<CommandType, Func<Command, Message>> Routes;
        private readonly IReceiver<Command> _receiver;
        private readonly ISender<Message> _sender;
        private readonly UdpClient _udpClient;
        public Server() { 
            _udpClient = new UdpClient(8808);
            Routes = new();
            _receiver = new Receiver<Command>(_udpClient);
            _sender = new Sender<Message>(_udpClient);
            Console.WriteLine("Сервер создан");
        }

        public bool Start()
        {
            _receiver.Start();
            _receiver.OnReceive += MessageReceivedEventHandler;
            Console.WriteLine("Сервер запущен");
            return true;
        }

        public void Stop()
        {
            _receiver.Stop();
            _receiver.OnReceive -= MessageReceivedEventHandler;
            Console.WriteLine("Сервер отключен");

        }

        public void AddRoute(CommandType key, Func<Command, Message> Handler)
        {
            Routes.Add(key, Handler);
        }

        private void MessageReceivedEventHandler(IPEndPoint endPoint, Command command)
        {
            Message message;
            try
            {
                if (Routes.TryGetValue(command.CommandType, out var handler))
                {
                    message = handler.Invoke(command);
                    Console.WriteLine("Message received");

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
                message = MessageBuilder.BuildException(ex);
            }
            _sender.Send(endPoint, message);
        }
    }
}
