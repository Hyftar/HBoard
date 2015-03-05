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
             new Point(7, 0)
        };

        private static readonly Point[] KnightPositions = new[]
        {
            new Point(1, 0),
            new Point(6, 0)
        };

        private static readonly Point[] BishopPositions = new[]
        {
            new Point(2, 0),
            new Point(5, 0)
        };

        private static readonly Point QueenPosition = new Point(3, 0);
        private static readonly Point KingPosition = new Point(4, 0);
        #endregion

        public static void GenerateClassicBoard(GameContext context, GameBoard board)
        {
            if (context.Players.Length != 2)
                throw new BoardGenerationException("A classic board may only be generated with two players.");

            ChessPlayer white = (ChessPlayer) context.Players.First(),
                        black = (ChessPlayer) context.Players.Last();

            for (int i = 0; i < board.Width; i++)
            {
                board[i, 1].Content = new PawnUnit(white);
                board[i, board.Height - 2].Content = new PawnUnit(black);
            }

            int cycles = board.Width / BOARD_WIDTH;
            for (int i = 0; i <= cycles; i++)
            {
                Point offset = new Point(i * BOARD_WIDTH, 0);

                board.AddUnit<RookUnit>(white, offset, ClassicBoard.RookPositions);
                board.AddUnit<RookUnit>(black, offset, ClassicBoard.RookPositions);

                board.AddUnit<KnightUnit>(white, offset, ClassicBoard.KnightPositions);
                board.AddUnit<KnightUnit>(black, offset, ClassicBoard.KnightPositions);

                board.AddUnit<BishopUnit>(white, offset, ClassicBoard.BishopPositions);
                board.AddUnit<BishopUnit>(black, offset, ClassicBoard.BishopPositions);

                board.AddUnit<QueenUnit>(white, offset, ClassicBoard.QueenPosition);
                board.AddUnit<QueenUnit>(black, offset, ClassicBoard.QueenPosition);

                board.AddUnit<KingUnit>(white, offset, ClassicBoard.KingPosition);
                board.AddUnit<KingUnit>(black, offset, ClassicBoard.KingPosition);              
            }
        }
    }
}
