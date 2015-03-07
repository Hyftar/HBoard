using System;
using System.Drawing;
using HBoard.Core;

namespace HBoard.Chess.Generation
{
    public static class GenerationHelper
    {
        public static void AddUnit<T>(this GameBoard board, Orientation orientation, ChessPlayer player, params Point[] relativePositions) where T : BoardUnit, new()
        {
            if (orientation == Orientation.Horizontal)
                AddUnitHorizontally<T>(board, player, Point.Empty, relativePositions);
            else
                AddUnitVertically<T>(board, player, Point.Empty, relativePositions);
        }

        public static void AddUnit<T>(this GameBoard board, Orientation orientation, ChessPlayer player, Point offset, params Point[] relativePositions) where T : BoardUnit, new()
        {
            if (orientation == Orientation.Horizontal)
                AddUnitHorizontally<T>(board, player, offset, relativePositions);
            else
                AddUnitVertically<T>(board, player, offset, relativePositions);
        }

        private static void AddUnitVertically<T>(this GameBoard board, ChessPlayer player, Point offset, params Point[] relativePositions) where T : BoardUnit, new()
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

        private static void AddUnitHorizontally<T>(this GameBoard board, ChessPlayer player, Point offset, params Point[] relativePositions) where T : BoardUnit, new()
        {
            if (board == null)
                throw new ArgumentNullException("board");

            foreach (Point point in relativePositions)
            {
                int x = player.Type == PlayerType.White ? point.X : board.Width - point.X - 1;
                if (offset.Y + point.Y > board.Height - 1 || offset.X + point.X > board.Width - 1)
                    return;

                board[offset.X + x, offset.Y + point.Y].Content = new T { Player = player };
            }
        }
    }
}
