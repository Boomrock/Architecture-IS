using NetProtocol;
using System.Net;
using System.Net.Sockets;

namespace NetController
{
    public class Server : IServer<Command , CommandType>
    {
        public readonly Dictionary<CommandType, Func<Command, Message>> Routes;
        private readonly IReceiver<Command> _receiver;
        private readonly ISender<Message> _sender;
        private readonly UdpClient _udpClient;
        NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public Server() { 
            _udpClient = new UdpClient(8808);
            Routes = new();
            _receiver = new Receiver<Command>(_udpClient);
            _sender = new Sender<Message>(_udpClient);
            Logger.Info("Сервер создан");
        }

        public bool Start()
        {
            _receiver.Start();
            _receiver.OnReceive += MessageReceivedEventHandler;
            Logger.Info("Сервер запущен");
            return true;
        }

        public void Stop()
        {
            _receiver.Stop();
            _receiver.OnReceive -= MessageReceivedEventHandler;
            Logger.Info("Сервер отключен");

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
                    Logger.Info("Message received");

                }
                else
                {
                    message = MessageBuilder
                        .BuildException(
                        $"dictionori doesn't contains commandHandler for {command.CommandType} \n");
                    Logger.Error(message.MessageBody);
                }
            }
            catch (Exception ex)
            {
                message = MessageBuilder.BuildException(ex);
                Logger.Error(ex.Message);

            }
            _sender.Send(endPoint, message);
        }
    }
}
