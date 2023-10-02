using System.Net;

namespace NetController
{
    public interface ISender<TMessage>
    {
        void Send(IPEndPoint endPoint, TMessage message);
    }
}
