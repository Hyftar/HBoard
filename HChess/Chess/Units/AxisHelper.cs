using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBoard.Chess.Logic;
using HBoard.Core;
using HBoard.Logic;

namespace HBoard.Chess.Units
{
    public static class AxisHelper
    {
        public static Boolean AdvancePosition(this ChessUnit unit, IPlayer player, Point currentPosition, ref Int32 metric, ref Point position, Boolean hasCrossedOrigin)
        {
            if (player == null)
            {
                if (metric++ == 0)
                    position = currentPosition;
            }
            else if (!hasCrossedOrigin)
            {
                position = currentPosition;
                metric = player == unit.Player ? 0 : 1;
            }
            else
            {
                if (player != unit.Player)
                    metric++;
                return true;
            }

            return false;
        }

        public static MovementPath GetPath(Point position, Int64 metric, Direction direction, MovementType movement = MovementType.All)
        {
            return new MovementPath(
                location: position,
                vectors: new SpecializedMovementVector(
                    direction: direction,
                    metric: metric,
                    movement: movement));
        }
    }
}
