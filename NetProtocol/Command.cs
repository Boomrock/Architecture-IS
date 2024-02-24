using NetController;

namespace NetProtocol
{
    public struct Command
    {
        public Command()
        {
            CommandType = CommandType.None;
            Data = new JsonDictionary();
        }
        public CommandType CommandType { get; set; }
        public JsonDictionary Data { get; set; }
    }

    public enum CommandType
    {
        None,
        Add,
        Save,
        Delete,
        TransferAll,
        TransferByIndex,
        Error
    }
}
