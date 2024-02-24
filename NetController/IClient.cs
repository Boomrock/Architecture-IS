using NetProtocol;

namespace NetController
{
    public interface IClient<MessageEnum, TMessage> where MessageEnum : Enum
    {
        public event Action<Command> OnReceive;

        public void Send(TMessage message);
        public void Close();
    }
}
