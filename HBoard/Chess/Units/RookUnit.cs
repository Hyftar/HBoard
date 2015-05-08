﻿using System;
using System.Collections.Generic;
using System.Drawing;
using ExtensionLib;
using HBoard.Core;
using HBoard.Logic;

namespace HBoard.Chess.Units
{
    /// <summary>
    /// Represents a <see cref="T:HBoard.Core.BoardUnit"/> of type rook.
    /// </summary>
    public class RookUnit : ChessUnit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:HBoard.Chess.RookUnit"/> class.
        /// </summary>
        public RookUnit() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:HBoard.Chess.RookUnit"/> class.
        /// </summary>
        public RookUnit(IPlayer player)
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
            Point horizontalLocation = Point.Empty;
            Point verticalLocation = Point.Empty;

            int horizontalMetric = 0;
            int verticalMetric = 0;

            var enumerator = board.Cells.GetArrayEnumerator();
            while (enumerator.MoveNext())
            {
                BoardCell cell = (BoardCell) enumerator.Current;
                Point currentPosition = new Point(enumerator.Positions[0], enumerator.Positions[1]);

                bool isOnVerticalAxis = currentPosition.Y == position.Y,
                     isOnHorizontalAxis = currentPosition.X == position.X;

                if (isOnHorizontalAxis && isOnVerticalAxis)
                    continue;

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
            }

            return new[]
            {
                AxisHelper.GetPath(horizontalLocation, horizontalMetric, AxisDirection.Horizontal),
                AxisHelper.GetPath(verticalLocation, verticalMetric, AxisDirection.Vertical)
            };
        }
    }
}
