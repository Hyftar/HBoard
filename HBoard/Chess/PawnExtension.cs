using System;

namespace HBoard.Chess
{
    [Flags]
    public enum PawnExtension
    {
        None =          0,
        EnPassant =     1 << 0,
        InitialShift =  1 << 1,
        All =          (1 << 2) - 1
    }
}
