using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBoard.Core;

namespace HBoard.Chess
{
    public class ChessOptions : GameOptions
    {
        public ChessOptions() : base()
        {
            this.CastlingPolicy = CastlingPolicy.Both;
            this.PawnExtraRules = PawnExtension.All;
        }

        public CastlingPolicy CastlingPolicy { get; set; }
        public PawnExtension PawnExtraRules { get; set; }
    }
}
