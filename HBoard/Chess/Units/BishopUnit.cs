using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using ExtensionLib;
using HBoard.Core;
using HBoard.Logic;

namespace HBoard.Chess.Units
{
    public class BishopUnit : ChessUnit
    {
        public BishopUnit() : base() { }

        public BishopUnit(IPlayer player)
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
            MovementPath[] paths = new MovementPath[2];

            Point primeLocation = Point.Empty;
            Point lastLocation = Point.Empty;

            int primeMetric = 0, lastMetric = 0;
            int primeAdjustment = position.Y - position.X;
            int lastAdjustment = position.Y + position.X;
            bool primeAxisResolved = false, lastAxisResolved = false;

            bool hasCrossedOrigin = false;

            var enumerator = board.Cells.GetArrayEnumerator();
            while (enumerator.MoveNext())
            {
                BoardCell cell = (BoardCell) enumerator.Current;
                Point currentPosition = new Point(enumerator.Positions[0], enumerator.Positions[1]);

                var cellContent = cell == null ? null : cell.Content;
                var cellPlayer = cellContent == null ? null : cell.Content.Player;

                bool isOnPrimeAxis = primeAdjustment + currentPosition.X == currentPosition.Y,
                     isOnLastAxis = lastAdjustment - currentPosition.X == currentPosition.Y;

                Debug.WriteLine("({0}, {1}): {2}", currentPosition.X, currentPosition.Y, currentPosition.X + primeAdjustment);
                if (isOnPrimeAxis && isOnLastAxis)
                    hasCrossedOrigin = true;
                else if (isOnPrimeAxis && !primeAxisResolved)
                    primeAxisResolved = this.AdvancePosition(cellPlayer, currentPosition, ref primeMetric, ref primeLocation, hasCrossedOrigin);
                else if (isOnLastAxis && !lastAxisResolved)
                    lastAxisResolved = this.AdvancePosition(cellPlayer, currentPosition, ref lastMetric, ref lastLocation, hasCrossedOrigin);
            }

            return new[]
            {
                AxisHelper.GetPath(primeLocation, primeMetric, Direction.Diagonal),
                AxisHelper.GetPath(lastLocation, lastMetric, Direction.Diagonal)
            };
        }
    }
}
