using System.Net;

namespace NetController
{
    internal interface IReceiver<TMessage> 
    {
        public event Action<IPEndPoint, TMessage> OnReceive;
        public void Start();
        public void Stop();
    }
}
