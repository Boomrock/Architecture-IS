using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetController
{
    internal interface IServer<TMessage, MessageType> where MessageType : Enum
    {
        public bool Start(Action RequestHandler);
        public void Stop();
        public void AddRoute(MessageType key, Func<TMessage, string> Handler);
    }
}
