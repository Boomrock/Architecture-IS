using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetController
{
    public class Sender<TMessage> : ISender<TMessage>
    {
        private readonly UdpClient _udpClient;

        public Sender(UdpClient udpClient)
        {
            _udpClient = udpClient;
        }
        public void Send(IPEndPoint endPoint, TMessage message)
        {
            try
            {
                var JsonString = JsonConvert.SerializeObject(message);
                var @byte = Encoding.UTF8.GetBytes(JsonString);
                Task.Factory.StartNew(() => _udpClient.SendAsync(@byte, endPoint));
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
