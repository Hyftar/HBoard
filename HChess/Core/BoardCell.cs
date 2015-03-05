using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace HBoard.Core
{
    /// <summary>
    /// Represents a cell contained within a <see cref="T:HBoard.Core.GameBoard"/>.
    /// </summary>
    public class BoardCell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:HBoard.Core.BoardCell"/> class.
        /// </summary>
        internal BoardCell() { }

        /// <summary>
        /// Gets or sets the <see cref="T:HBoard.Core.BoardUnit"/> held within this cell.
        /// </summary>
        public BoardUnit Content { get; set; }

        //public Point Location { get; internal set; }

        public override String ToString()
        {
            if (this.Content == null)
                return "{Unit: null, Player: null}";

            return String.Format("{{Unit: {0}, Player: {1}}}", this.Content.GetType().Name, this.Content.Player);
        }
    }
}
