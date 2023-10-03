using NetProtocol;
using System.Net;
using System.Net.Sockets;

namespace NetController
{
    public class Client : IClient<Command>
    {
        public event Action<Message> OnReceive;

        private readonly UdpClient _udpClient;
        private readonly ISender<Command> _sender;
        private readonly IReceiver<Message> _receiver;
        private readonly IPEndPoint _endPoint;

        public Client(IPEndPoint endPoint)
        {
            _udpClient = new UdpClient(5555);
            _sender = new Sender<Command>(_udpClient);
            _receiver = new Receiver<Message>(_udpClient);
            _receiver.Start();
            _receiver.OnReceive += (_, message) => OnReceive?.Invoke(message);
            _endPoint = endPoint;
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
