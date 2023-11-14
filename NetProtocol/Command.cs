using NetController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        Delete,
        TransferAll,
        TransferByIndex,
        Error
    }
}
