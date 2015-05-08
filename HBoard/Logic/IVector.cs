using System;

namespace HBoard.Logic
{
    public interface IVector
    {
        AxisDirection Direction { get; }
        Int64 Length { get; }
    }
}
