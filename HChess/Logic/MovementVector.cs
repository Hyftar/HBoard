﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBoard.Logic
{
    public struct MovementVector : IVector
    {
        public MovementVector(Direction direction, Int64 metric)
            : this()
        {
            this.Direction = direction;
            this.Length = metric;
        }

        public Direction Direction { get; private set; }
        public Int64 Length { get; private set; }
    }
}
