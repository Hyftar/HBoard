using System;
using System.Collections.Generic;
using System.Drawing;
using ExtensionLib;
using HBoard.Core;
using HBoard.Logic;

namespace HBoard.Chess.Units
{
    /// <summary>
    /// Represents a <see cref="T:HBoard.Core.BoardUnit"/> of type queen.
    /// </summary>
    public class QueenUnit : ChessUnit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:HBoard.Chess.QueenUnit"/> class.
        /// </summary>
        public QueenUnit() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:HBoard.Chess.QueenUnit"/> class.
        /// </summary>
        public QueenUnit(IPlayer player)
            : base(player) { }

        /// <summary>
        /// Determines whether the chess unit can be moved to a specific cell.
        /// </summary>
        /// <param name="board">The container board to use to determine movement possibilities.</param>
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
            Point primeLocation = Point.Empty, // First diagonal position
                  lastLocation = Point.Empty,  // Second diagonal position
                  horizontalLocation = Point.Empty,
                  verticalLocation = Point.Empty;

            int primeMetric = 0, lastMetric = 0;
            int primeAdjustment = position.Y - position.X,
                lastAdjustment = position.Y + position.X;

            int horizontalMetric = 0, verticalMetric = 0;

            var enumerator = board.Cells.GetArrayEnumerator();
            while (enumerator.MoveNext())
            {
                BoardCell cell = (BoardCell) enumerator.Current;
                Point currentPosition = new Point(enumerator.Positions[0], enumerator.Positions[1]);

                bool isOnVerticalAxis = currentPosition.Y == position.Y,
                     isOnHorizontalAxis = currentPosition.X == position.X,
                     isOnPrimeAxis = primeAdjustment + currentPosition.X == currentPosition.Y,
                     isOnLastAxis = lastAdjustment - currentPosition.X == currentPosition.Y;

                if (isOnLastAxis && isOnPrimeAxis)
                    continue;

                else if (isOnPrimeAxis)
                {
                    if (primeMetric++ == 0)
                        primeLocation = currentPosition;
                }

                else if (isOnLastAxis)
                {
                    if (lastMetric++ == 0)
                        lastLocation = currentPosition;
                }
                else if (isOnVerticalAxis)
                {
                    if (verticalMetric++ == 0)
                        verticalLocation = currentPosition;
                }
                else if (isOnHorizontalAxis)
                {
                    if (horizontalMetric++ == 0)
                        horizontalLocation = currentPosition;
                }

                /*
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
                 */
            }

            return new[]
            {
                AxisHelper.GetPath(primeLocation, primeMetric, AxisDirection.PrimeDiagonal),
                AxisHelper.GetPath(lastLocation, lastMetric, AxisDirection.LastDiagonal),
                AxisHelper.GetPath(horizontalLocation, horizontalMetric, AxisDirection.Horizontal),
                AxisHelper.GetPath(verticalLocation, verticalMetric, AxisDirection.Vertical)
            };
        }
    }
}
