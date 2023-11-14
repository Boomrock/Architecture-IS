using NetProtocol;
using System.Net;
using System.Net.Sockets;

namespace NetController
{
    public class Server : IServer<Command , CommandType>
    {
        public readonly Dictionary<CommandType, Func<Command, Command>> Routes;
        private readonly IReceiver<Command> _receiver;
        private readonly ISender<Command> _sender;
        private readonly UdpClient _udpClient;
        private readonly CancellationToken _cancellationToken;
        private readonly Handler<Command> _handler;
        NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public Server() {

            _cancellationToken = new CancellationToken();
            _udpClient = new UdpClient(8808);

            _receiver = new Receiver<Command>(_udpClient, _cancellationToken);
            _sender = new Sender<Command>(_udpClient);

            _handler = new Handler<Command>(
                _receiver, 
                _cancellationToken, 
                4, 
                MessageReceivedEventHandler);

            Routes = new();

            Logger.Info("Сервер создан");
        }

        public bool Start()
        {
            _receiver.Start();
            Logger.Info("Сервер запущен");
            return true;
        }

        public void Stop()
        {
            _receiver.Stop();
            Logger.Info("Сервер отключен");

        }

        public void AddRoute(CommandType key, Func<Command, Command> Handler)
        {
            Routes.Add(key, Handler);
        }

        private void MessageReceivedEventHandler(IPEndPoint endPoint, Command command)
        {
            Command message;
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
                    Logger.Error(message.Data.Get<string>("Error"));
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
