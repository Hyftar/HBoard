using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBoard.Chess;
using HBoard.Chess.Units;
using HBoard.Core;

namespace HBoard.Chess.Generation
{
    public static class ClassicBoard
    {
        public const Int32 BOARD_WIDTH = 8,
                           BOARD_HEIGHT = 8;

        #region Relative unit positions
        public static readonly Point[] RookPositions = new[]
        {
             new Point(0, 0),
             new Point(0, 7)
        };

        private static readonly Point[] KnightPositions = new[]
        {
            new Point(0, 1),
            new Point(0, 6)
        };

        private static readonly Point[] BishopPositions = new[]
        {
            new Point(0, 2),
            new Point(0, 5)
        };

        private static readonly Point QueenPosition = new Point(0, 4);
        private static readonly Point KingPosition = new Point(0, 3);
        #endregion

        public static void GenerateClassicBoard(GameContext context, GameBoard board)
        {
            if (context.Players.Length != 2)
                throw new BoardGenerationException("A classic board may only be generated with two players.");

            ChessPlayer white = (ChessPlayer) context.Players.First(),
                        black = (ChessPlayer) context.Players.Last();

            for (int i = 0; i < board.Height; i++)
            {
                board[1, i].Content = new PawnUnit(white);
                board[board.Width - 2, i].Content = new PawnUnit(black);
            }

            int cycles = board.Width / BOARD_WIDTH;
            for (int i = 0; i <= cycles; i++)
            {
                Point offset = new Point(0, i * BOARD_HEIGHT);

                board.AddUnit<RookUnit>(Orientation.Horizontal, white, offset, ClassicBoard.RookPositions);
                board.AddUnit<RookUnit>(Orientation.Horizontal, black, offset, ClassicBoard.RookPositions);

                board.AddUnit<KnightUnit>(Orientation.Horizontal, white, offset, ClassicBoard.KnightPositions);
                board.AddUnit<KnightUnit>(Orientation.Horizontal, black, offset, ClassicBoard.KnightPositions);

                board.AddUnit<BishopUnit>(Orientation.Horizontal, white, offset, ClassicBoard.BishopPositions);
                board.AddUnit<BishopUnit>(Orientation.Horizontal, black, offset, ClassicBoard.BishopPositions);

                board.AddUnit<QueenUnit>(Orientation.Horizontal, white, offset, ClassicBoard.QueenPosition);
                board.AddUnit<QueenUnit>(Orientation.Horizontal, black, offset, ClassicBoard.QueenPosition);

                board.AddUnit<KingUnit>(Orientation.Horizontal, white, offset, ClassicBoard.KingPosition);
                board.AddUnit<KingUnit>(Orientation.Horizontal, black, offset, ClassicBoard.KingPosition);              
            }
        }
    }
}
