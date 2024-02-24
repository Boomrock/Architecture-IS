using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_Architecture_IS.IOController.Interfaces
{
    interface IWriter<TData>
    {
        void Write(TData[] data);
    }
}
