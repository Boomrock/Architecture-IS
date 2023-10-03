using NetProtocol;

namespace NetController
{
    public interface IClient<TMessage>
    {
        public event Action<Message> OnReceive;
        public void Send(TMessage message);
        public void Close();
    }
}
