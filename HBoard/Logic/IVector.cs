using System;

namespace HBoard.Logic
{
    public interface IVector
    {
        Direction Direction { get; }
        Int64 Length { get; }
    }
}
