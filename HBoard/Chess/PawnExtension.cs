using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HBoard.Chess
{
    [Flags]
    public enum PawnExtension
    {
        None =           0,
        EnPassant =      1 << 0, //    1
        InitialShift =   1 << 1, //   10
        All =          ~(1 << 2) //   11
    }
}
