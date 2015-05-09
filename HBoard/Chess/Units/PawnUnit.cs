using System;
using System.Collections.Generic;
using System.Drawing;
using HBoard.Core;
using HBoard.Logic;

namespace HBoard.Chess.Units
{
    /// <summary>
    /// Represents a <see cref="T:HBoard.Core.BoardUnit"/> of type pawn.
    /// </summary>
    public class PawnUnit : ChessUnit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:HBoard.Chess.PawnUnit"/> class.
        /// </summary>
        public PawnUnit() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:HBoard.Chess.PawnUnit"/> class.
        /// </summary>
        public PawnUnit(IPlayer player)
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
            var player = (ChessPlayer) this.Player;
            Point vectorStart = position;

            if (player.Direction.Orientation == Orientation.Vertical)
                vectorStart.X += position.X + player.Direction.Direction == Direction.Forward ? 1 : -1;
            else if (player.Direction.Orientation == Orientation.Horizontal)
                vectorStart.Y += player.Direction.Direction == Direction.Forward ? 1 : -1;

            return null;
        }

        internal override void OnMove(GameBoard board, Point origin, Point target) { }
    }
}
