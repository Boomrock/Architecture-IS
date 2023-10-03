using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProtocol
{
    public struct Command
    {
        public CommandType CommandType { get; set; }
        public Dictionary<Type, Object> Data { get; set; }
    }

    public enum CommandType
    {
        Add,
        Delete,
        TransferAll,
        TransferByIndex
    }
}
