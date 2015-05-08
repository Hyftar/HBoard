using System;

namespace HBoard.Logic
{
    public struct MovementVector : IVector
    {
        public MovementVector(AxisDirection direction, Int64 metric)
            : this()
        {
            this.Direction = direction;
            this.Length = metric;
        }

        public AxisDirection Direction { get; private set; }
        public Int64 Length { get; private set; }
    }
}
