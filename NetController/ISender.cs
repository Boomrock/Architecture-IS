using System.Net;

namespace NetController
{
    internal interface ISender<TMessage>
    {
        bool Send(IPEndPoint endPoint, TMessage message);
    }
}
