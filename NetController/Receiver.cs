using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using NetProtocol;
using NLog;

namespace NetController
{
    public class Receiver<TMessage> : IReceiver<TMessage>
    {
        private readonly UdpClient _udpClient;
        private readonly CancellationToken _cancellationToken;
        private readonly ConcurrentQueue<(IPEndPoint, TMessage)> _queue; // очередь для хранения сообщений

        private readonly Logger _logger = LogManager.GetCurrentClassLogger();


        public Receiver(UdpClient udpClient, CancellationToken cancellationToken)
        {
            _udpClient = udpClient;
            _cancellationToken = cancellationToken;
            _queue = new ConcurrentQueue<(IPEndPoint, TMessage)>();
        }

        public void Start()
        {
            _ = Task.Factory.StartNew(() => Receiv());
        }

        private async void Receiv()
        {
            try
            {
                while (true)
                {
                    var result = await _udpClient.ReceiveAsync(_cancellationToken);
                    var JsonString = Encoding.UTF8.GetString(result.Buffer);
                    var message = JsonSerializer.Deserialize<TMessage>(JsonString);
                    _queue.Enqueue((result.RemoteEndPoint, message)); // добавляем сообщение в очередь
                }
            }
            catch (OperationCanceledException)
            {
                // обработка отмены операции
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        public void Stop()
        {
            _udpClient.Close();
        }

        public bool TryDequeue(out (IPEndPoint, TMessage) message)
        {
            return _queue.TryDequeue(out message); // пытаемся извлечь сообщение из очереди
        }
    }
}
