﻿using HBoard.Core;

namespace HBoard.Chess
{
    public class ChessOptions : GameOptions
    {
        public ChessOptions()
        {
            this.CastlingPolicy = CastlingPolicy.Both;
            this.PawnExtraRules = PawnExtension.All;
        }

        public CastlingPolicy CastlingPolicy { get; set; }
        public PawnExtension PawnExtraRules { get; set; }
        public int TimeLimit { get; set; }
        public int TimeIncrement { get; set; }
    }
}
