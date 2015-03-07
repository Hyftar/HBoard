using System;
using System.Collections.Generic;
using System.Drawing;
using ExtensionLib;
using HBoard.Core;
using HBoard.Logic;

namespace HBoard.Chess.Units
{
    public class QueenUnit : ChessUnit
    {
        public QueenUnit() : base() { }

        public QueenUnit(IPlayer player)
            : base(player) { }

        /// <summary>
        /// Determines whether the chess unit can be moved to a specific cell.
        /// </summary>
        /// <param name="origin">The location of the cell holding the unit.</param>
        /// <param name="target">The location of the targetted cell.</param>
        /// <returns>A boolean value indicating whether the unit is able to move to a given cell.</returns>
        public override Boolean CanMove(GameBoard board, Point origin, Point target)
        {
            BoardCell targetCell = board.Cells[target.X, target.Y];

            if (targetCell.Content.Player == this.Player)
                return false;

            return true;
        }

        public override IEnumerable<MovementPath> GetMovementPaths(GameBoard board, Point position)
        {
            Point primePosition = Point.Empty, // First diagonal position
                  lastPosition = Point.Empty,  // Second diagonal position
                  horizontalPosition = Point.Empty,
                  verticalPosition = Point.Empty;

            int primeMetric = 0, lastMetric = 0;
            int primeAdjustment = position.Y - position.X,
                lastAdjustment = position.Y + position.X;
            bool primeAxisResolved = false, lastAxisResolved = false;

            int horizontalMetric = 0, verticalMetric = 0;
            bool horizontalAxisResolved = false, verticalAxisResolved = false;

            bool hasCrossedOrigin = false;

            var enumerator = board.Cells.GetArrayEnumerator();
            while (enumerator.MoveNext())
            {
                BoardCell cell = (BoardCell) enumerator.Current;
                Point currentPosition = new Point(enumerator.Positions[0], enumerator.Positions[1]);

                var cellContent = cell == null ? null : cell.Content;
                var cellPlayer = cellContent == null ? null : cell.Content.Player;

                bool isOnVerticalAxis = currentPosition.Y == position.Y,
                     isOnHorizontalAxis = currentPosition.X == position.X,
                     isOnPrimeAxis = primeAdjustment + currentPosition.X == currentPosition.Y,
                     isOnLastAxis = lastAdjustment - currentPosition.X == currentPosition.Y;

                if (isOnLastAxis && isOnPrimeAxis)
                    hasCrossedOrigin = true;
                else if (isOnPrimeAxis && !primeAxisResolved)
                    primeAxisResolved = this.AdvancePosition(cellPlayer, currentPosition, ref primeMetric, ref primePosition, hasCrossedOrigin);
                else if (isOnLastAxis && !lastAxisResolved)
                    lastAxisResolved = this.AdvancePosition(cellPlayer, currentPosition, ref lastMetric, ref lastPosition, hasCrossedOrigin);
                else if (isOnVerticalAxis && !verticalAxisResolved)
                    verticalAxisResolved = this.AdvancePosition(cellPlayer, currentPosition, ref verticalMetric, ref verticalPosition, hasCrossedOrigin);
                else if (isOnHorizontalAxis && !horizontalAxisResolved)
                    horizontalAxisResolved =  this.AdvancePosition(cellPlayer, currentPosition, ref horizontalMetric, ref horizontalPosition, hasCrossedOrigin);
            }

            return new[]
            {
                AxisHelper.GetPath(primePosition, primeMetric, Direction.Diagonal),
                AxisHelper.GetPath(lastPosition, lastMetric, Direction.Diagonal),
                AxisHelper.GetPath(horizontalPosition, horizontalMetric, Direction.Horizontal),
                AxisHelper.GetPath(verticalPosition, verticalMetric, Direction.Vertical)
            };
        }
    }
}
