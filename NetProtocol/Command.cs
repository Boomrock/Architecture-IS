using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProtocol
{
    public struct Command
    {
        public CommandType CommandType;
        public Dictionary<Type, Object> Data; 
    }

    public enum CommandType
    {
        Add,
        Deleate,
    }
}
