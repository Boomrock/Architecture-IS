using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetController
{
    public class Handler<TMessage>
    {
        private readonly IReceiver<TMessage> _receiver;
        private readonly CancellationToken _cancellationToken;
        private readonly int _maxDegreeOfParallelism; // максимальное количество параллельных задач
        private readonly Action<IPEndPoint, TMessage> _messageHandler;

        public Handler(IReceiver<TMessage> receiver, CancellationToken cancellationToken, int maxDegreeOfParallelism, Action<IPEndPoint, TMessage> MessageHandler)
        {
            _receiver = receiver;
            _cancellationToken = cancellationToken;
            _maxDegreeOfParallelism = maxDegreeOfParallelism;
            _messageHandler = MessageHandler;
        }

        public void Start()
        {
            _ = Task.Factory.StartNew(() => Process());
        }

        private async void Process()
        {
            var semaphore = new SemaphoreSlim(_maxDegreeOfParallelism); // семафор для ограничения параллелизма
            var tasks = new List<Task>(); // список для хранения задач
            try
            {
                while (true)
                {
                    if (_receiver.TryDequeue(out var message)) // если есть сообщение в очереди
                    {
                        await semaphore.WaitAsync(_cancellationToken); // ждем, пока освободится слот для новой задачи
                        var task = Task.Run(() => HandleMessage(message), _cancellationToken); // запускаем задачу для обработки сообщения
                        tasks.Add(task); // добавляем задачу в список
                        _ = task.ContinueWith(t => semaphore.Release()); // освобождаем слот после завершения задачи
                    }
                    else
                    {
                        await Task.Delay(100, _cancellationToken); // ждем некоторое время, если нет сообщений в очереди
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // обработка отмены операции
            }
            catch (Exception ex)
            {
                // обработка других исключений
            }
            finally
            {
                await Task.WhenAll(tasks); // дожидаемся завершения всех задач
            }
        }

        private void HandleMessage((IPEndPoint, TMessage) message)
        {
            _messageHandler?.Invoke(message.Item1, message.Item2);
        }
    }
}
