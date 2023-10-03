namespace NetProtocol
{
    [Serializable]
    public struct Message
    {
        public MessageType MessageType { get; set; }
        public string MessageBody { get; set; }
    }
    public enum MessageType
    {
        Transfer, 
        Error,
    }
}