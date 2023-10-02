using NetProtocol;
using System.Net;
using System.Net.Sockets;

namespace NetController
{
    public class Client : IClient<Command>
    {
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
            _receiver.OnReceive += ReceiveHandeler;
            _endPoint = endPoint;
        }

        private void ReceiveHandeler(System.Net.IPEndPoint arg1, Message arg2)
        {
        }

        public void Close()
        {
            _udpClient.Close();
            _receiver.Stop();
            _receiver.OnReceive -= ReceiveHandeler;
        }

        public void Send(Command message)
        {
            _sender.Send(_endPoint, message);
        }
    }
}
