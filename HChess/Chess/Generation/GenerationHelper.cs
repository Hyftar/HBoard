using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBoard.Core;

namespace HBoard.Chess.Generation
{
    public static class GenerationHelper
    {
        public static void AddUnit<T>(this GameBoard board, ChessPlayer player, params Point[] relativePositions) where T : BoardUnit, new()
        {
            AddUnit<T>(board, player, Point.Empty, relativePositions);
        }

        public static void AddUnit<T>(this GameBoard board, ChessPlayer player, Point offset, params Point[] relativePositions) where T : BoardUnit, new()
        {
            if (board == null)
                throw new ArgumentNullException("board");

            foreach (Point point in relativePositions)
            {
                int y = player.Type == PlayerType.White ? point.Y : board.Height - point.Y - 1;
                if (offset.X + point.X > board.Width - 1 || offset.Y + point.Y > board.Height - 1)
                    return;

                board[offset.X + point.X, offset.Y + y].Content = new T { Player = player };
            }
        }
    }
}
