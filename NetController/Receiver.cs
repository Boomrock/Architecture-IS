using System.Net;
using System.Net.Sockets;
using System.Text;
using NetProtocol;
using Newtonsoft.Json;

namespace NetController
{
    public class Receiver<TMessage> : IReceiver<TMessage>
    {
        private readonly UdpClient _udpClient;
        private readonly CancellationTokenSource _token;

        public event Action<IPEndPoint, TMessage> OnReceive;
        NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public Receiver(UdpClient udpClient)
        {
            _udpClient = udpClient;
            _token = new CancellationTokenSource();
        }

        public void Start()
        {

            _ = Task
                .Factory
                .StartNew(() => Receiv(_token));
            Logger.Info  ("Receiver запущен");
        }

        private async void Receiv(CancellationTokenSource cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var result = await _udpClient.ReceiveAsync();
                    var JsonString = Encoding.UTF8.GetString(result.Buffer);
                    var message = JsonConvert.DeserializeObject<TMessage>(JsonString);
                    lock (OnReceive)
                    {
                        OnReceive?.Invoke(result.RemoteEndPoint, message);
                    };
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

        }
        public void Stop()
        {
            _token.Cancel();
            _udpClient.Close();
        }
    }
}
