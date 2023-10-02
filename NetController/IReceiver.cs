using System.Net;

namespace NetController
{
    public interface IReceiver<TMessage> 
    {
        public event Action<IPEndPoint, TMessage> OnReceive;
        public void Start();
        public void Stop();
    }
}
