using System.Collections.Generic;
using System.Drawing;
using HBoard.Core;
using HBoard.Logic;

namespace HBoard.Chess.Units
{
    public abstract class ChessUnit : BoardUnit
    {
        public ChessUnit() 
            : base() 
        {
            this.HasMoved = false;
        }

        public ChessUnit(IPlayer player)
            : base(player) 
        {
            this.HasMoved = false;

        }

        public IEnumerable<MovementPath> MovementsCache { get; private set; }
        public bool HasMoved { get; private set; }

        public void InvalidateCache()
        {
            this.MovementsCache = null;
        }

        public void GenerateCache(GameBoard board, Point position)
        {
            this.MovementsCache = this.GetMovementPaths(board, position);
        }

        internal virtual void OnMove(GameBoard board, Point origin, Point target) 
        {
            this.HasMoved = true;
        }

        public abstract IEnumerable<MovementPath> GetMovementPaths(GameBoard board, Point position);
    }
}
