using System.Net;
using System.Net.Sockets;

namespace NetController
{
    internal class Receiver<TMessage> : IReceiver<TMessage>
    {
        private readonly UdpClient _udpClient;

        public event Action<IPEndPoint, TMessage> MessageReceivedEvent;

        public Receiver(UdpClient udpClient)
        {
            this._udpClient = udpClient;
        }

        public void Start(Action RequestHandler)
        {

        }

        public void Stop()
        {

        }
    }
}
