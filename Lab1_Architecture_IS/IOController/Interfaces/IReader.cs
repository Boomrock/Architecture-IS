using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1_Architecture_IS.IOController.Interfaces
{
    internal interface IReader<TData>
    {
        TData[] Read();
    }
}
