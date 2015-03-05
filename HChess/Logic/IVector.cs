using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBoard.Logic
{
    public interface IVector
    {
        Direction Direction { get; }
        Int64 Length { get; }
    }
}
