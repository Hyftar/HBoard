using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using ExtensionLib;
using HBoard.Core;
using HBoard.Logic;

namespace HBoard.Chess.Units
{
    /// <summary>
    /// Represents a <see cref="T:HBoard.Core.BoardUnit"/> of type bishop.
    /// </summary>
    public class BishopUnit : ChessUnit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:HBoard.Chess.BishopUnit"/> class.
        /// </summary>
        public BishopUnit() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:HBoard.Chess.BishopUnit"/> class.
        /// </summary>
        public BishopUnit(IPlayer player) : base(player) { }

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
            MovementPath[] paths = new MovementPath[2];

            Point primeLocation = Point.Empty;
            Point lastLocation = Point.Empty;

            int primeMetric = 0, lastMetric = 0;
            int primeAdjustment = position.Y - position.X;
            int lastAdjustment = position.Y + position.X;

            var enumerator = board.Cells.GetArrayEnumerator();
            while (enumerator.MoveNext())
            {
                BoardCell cell = (BoardCell) enumerator.Current;
                Point currentPosition = new Point(enumerator.Positions[0], enumerator.Positions[1]);

                bool isOnPrimeAxis = primeAdjustment + currentPosition.X == currentPosition.Y,
                     isOnLastAxis = lastAdjustment - currentPosition.X == currentPosition.Y;

                Debug.WriteLine("({0}, {1}): {2}", currentPosition.X, currentPosition.Y, currentPosition.X + primeAdjustment);

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
            }

            return new[]
            {
                AxisHelper.GetPath(primeLocation, primeMetric, AxisDirection.PrimeDiagonal),
                AxisHelper.GetPath(lastLocation, lastMetric, AxisDirection.LastDiagonal)
            };
        }
    }
}
