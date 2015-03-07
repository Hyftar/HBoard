using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBoard.Core
{
    /// <summary>
    /// Represents the game configuration that holds settings relative to various aspects of the board and interaction thereof.
    /// </summary>
    public class GameOptions
    {
        public GameOptions()
        {
            this.BoardSize = new Size(8, 8);
        }

        public Action<GameContext, GameBoard> PopulateBoardDelegate { get; set; }

        public Size BoardSize { get; set; }
    }
}
