using System;
using System.Collections.Generic;
using System.Diagnostics;
using HBoard.Core;
using HBoard.Logic;

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
        
        public PlayerType Type { get; private set; }
        
        public CheckState Check { get; internal set; }

        public AbsoluteDirection Direction { get; private set; }

        public override String ToString()
        {
            return this.Type.ToString();
        }
    }
}
