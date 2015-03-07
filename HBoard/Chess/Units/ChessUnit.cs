﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBoard.Core;
using HBoard.Logic;

namespace HBoard.Chess.Units
{
    public abstract class ChessUnit : BoardUnit
    {
        public ChessUnit() : base() { }

        public ChessUnit(IPlayer player)
            : base(player) { }

        public IEnumerable<MovementPath> MovementsCache { get; private set; }

        public void InvalidateCache()
        {
            this.MovementsCache = null;
        }

        public void GenerateCache(GameBoard board, Point position)
        {
            this.MovementsCache = this.GetMovementPaths(board, position);
        }

        public abstract IEnumerable<MovementPath> GetMovementPaths(GameBoard board, Point position);
    }
}