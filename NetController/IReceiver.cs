using System.Net;

namespace NetController
{
    public interface IReceiver<TMessage> 
    {
        public void Start();
        public void Stop();
        bool TryDequeue(out (IPEndPoint, TMessage) message);
    }
}
