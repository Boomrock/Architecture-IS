
using NetProtocol;
using System.Net;
using System.Net.Sockets;

namespace NetController
{
    public class Client : IClient<CommandType, Command>
    {
        public event Action<Command> OnReceive;

        private readonly UdpClient _udpClient;
        private readonly ISender<Command> _sender;
        private readonly IReceiver<Command> _receiver;
        private readonly IPEndPoint _endPoint;
        private readonly Handler<Command> _handler;
        private CancellationToken _cancellationToken;

        public Client(IPEndPoint endPoint)
        {
            _udpClient = new UdpClient(5555);

            _sender = new Sender<Command>(_udpClient);
            _receiver = new Receiver<Command>(_udpClient, _cancellationToken);
            _handler = new Handler<Command>(_receiver, _cancellationToken, 4, OnReceiveHandler);

            _receiver.Start();
            _endPoint = endPoint;

        }

        private void OnReceiveHandler(IPEndPoint endPoint, Command message)
        {
            OnReceive?.Invoke(message);
        }

        public void Close()
        {
            _udpClient.Close();
            _receiver.Stop();
        }

        public void Send(Command message)
        {
            _sender.Send(_endPoint, message);
        }


    }
}
