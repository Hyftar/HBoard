using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace HBoard.Core
{
    /// <summary>
    /// Represents a logical board unit.
    /// </summary>
    public abstract class BoardUnit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:HBoard.Core.BoardUnit"/> class.
        /// </summary>
        protected BoardUnit() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:HBoard.Core.BoardUnit"/> class.
        /// </summary>
        protected BoardUnit(IPlayer player)
        {
            this.Player = player;
        }

        /// <summary>
        /// Gets the <see cref="T:HBoard.Core.IPlayer"/> that owns this unit.
        /// </summary>
        public IPlayer Player { get; set; }

        /// <summary>
        /// Determines whether the chess unit can be moved to a specific cell.
        /// </summary>
        /// <param name="board">The board that holds the cells.</param>
        /// <param name="target">The target cell.</param>
        /// <returns>A boolean value indicating whether the unit is able to move to a given cell.</returns>
        public abstract Boolean CanMove(GameBoard board, Point origin, Point target);
    }
}
