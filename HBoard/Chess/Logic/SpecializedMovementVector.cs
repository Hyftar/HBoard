using System;
using HBoard.Logic;

namespace HBoard.Chess.Logic
{
    public struct SpecializedMovementVector : IVector
    {
        public SpecializedMovementVector(AxisDirection direction, Int64 metric, MovementType movement)
            : this()
        {
            this.Direction = direction;
            this.Length = metric;
            this.Movement = movement;
        }

        public AxisDirection Direction { get; private set; }

        public Int64 Length { get; private set; }

        public MovementType Movement { get; private set; }
    }
}
