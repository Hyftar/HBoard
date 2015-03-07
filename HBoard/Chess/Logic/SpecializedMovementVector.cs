using System;
using HBoard.Logic;

namespace HBoard.Chess.Logic
{
    public struct SpecializedMovementVector : IVector
    {
        public SpecializedMovementVector(Direction direction, Int64 metric, MovementType movement)
            : this()
        {
            this.Direction = direction;
            this.Length = metric;
            this.Movement = movement;
        }

        public Direction Direction { get; private set; }

        public Int64 Length { get; private set; }

        public MovementType Movement { get; private set; }
    }
}
