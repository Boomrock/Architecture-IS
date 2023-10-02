namespace NetProtocol
{
    public struct Message
    {
        public MessageType MessageType;
        public string MessageBody;
    }
    public enum MessageType
    {
        Message, 
        Error,
    }
}