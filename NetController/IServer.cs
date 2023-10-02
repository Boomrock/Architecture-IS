using NetProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetController
{
    public interface IServer<TMessage, MessageType> where MessageType : Enum
    {
        public bool Start();
        public void Stop();
        public void AddRoute(MessageType key, Func<TMessage, Message> Handler);
    }
}
