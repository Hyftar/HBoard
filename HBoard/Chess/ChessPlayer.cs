using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBoard.Core;

namespace HBoard.Chess
{
    public class ChessPlayer : IPlayer
    {
        protected ChessPlayer()
        {
            this.CapturedUnits = new List<BoardUnit>();
            this.Stopwatch = new Stopwatch();
        }

        public ChessPlayer(PlayerType type)
            : this()
        {
            this.Type = type;
        }

        public Boolean Eliminated { get; set; }

        public List<BoardUnit> CapturedUnits { get; private set; }
        
        public Stopwatch Stopwatch { get; private set; }
        
        public PlayerType Type { get; set; }
        
        public CheckState Check { get; set; }

        /*public Boolean IsEliminated(GameBoard board)
        {
            if (board.Any(x => x.Content != null
                && x.Content is Chess.Units.KingUnit
                && ((ChessPlayer) x.Content.Player).Type == this.Type))
            {
                this.Eliminated = false;
                return this.Eliminated;
            }
            this.Eliminated = true;
            return Eliminated;
        }*/

        public override String ToString()
        {
            return this.Type.ToString();
        }
    }
}
