using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetController
{
    public interface IClient<TMessage>
    {
        public void Send(TMessage message);
        public void Close();
    }
}
