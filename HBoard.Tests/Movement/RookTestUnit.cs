using System;
using System.Drawing;
using System.Linq;
using System.IO;
using ExtensionLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HBoard.Core;
using HBoard.Chess;
using HBoard.Chess.Generation;
using HBoard.Chess.Units;
using HBoard.Logic;

namespace HBoard.Tests.Movement
{
    [TestClass]
    public class RookTestUnit
    {
        [TestMethod]
        public void TestClassicDefault()
        {
            var options = new GameOptions
            {
                PopulateBoardDelegate = ClassicBoard.GenerateClassicBoard,
                BoardSize = new Size(ClassicBoard.BOARD_WIDTH, ClassicBoard.BOARD_HEIGHT)
            };

            var gameContext = new GameContext(options, new[]
            {
                new ChessPlayer(PlayerType.White),
                new ChessPlayer(PlayerType.Black)
            });
            gameContext.Init();

            var rook = gameContext.Board
                .Select((x, i) => new { Index = i, Cell = x == null ? null : x.Content as RookUnit })
                .Where(x => x.Cell != null)
                .First();

            var path = rook.Cell.GetMovementPaths(gameContext.Board, new Point(rook.Index / 8, rook.Index % 8)).First();

            Assert.AreEqual(0, path.First().Length);
            Assert.AreEqual(0, path.Last().Length);
        }

        [TestMethod]
        public void TestAxes()
        {
            var options = new GameOptions
            {
                PopulateBoardDelegate = delegate(GameContext context, GameBoard board)
                {
                    ChessPlayer white = (ChessPlayer) context.Players.First(),
                                black = (ChessPlayer) context.Players.Last();

                    board.AddUnit<RookUnit>(Orientation.Horizontal, white, Point.Empty, new Point(3, 4));
                    board.AddUnit<BishopUnit>(Orientation.Horizontal, white, Point.Empty, new Point(2, 4));
                    board.AddUnit<PawnUnit>(Orientation.Horizontal, black, Point.Empty, new Point(4, 6));
                },
                BoardSize = new Size(8, 8)
            };

            var gameContext = new GameContext(options, new[]
            {
                new ChessPlayer(PlayerType.White),
                new ChessPlayer(PlayerType.Black)
            });
            gameContext.Init();

            var rook = gameContext.Board
                .Select((x, i) => new { Index = i, Cell = x == null ? null : x.Content as RookUnit })
                .Where(x => x.Cell != null)
                .First();

            var paths = rook.Cell.GetMovementPaths(gameContext.Board, new Point(rook.Index / 8, rook.Index % 8));
            MovementPath horizontalPath = paths.First(), verticalPath = paths.Last();

            // Horizontal axis
            Assert.AreEqual(horizontalPath.First().Direction, Direction.Horizontal);
            Assert.AreEqual(6, horizontalPath.First().Length);
            Assert.AreEqual(new Point(3, 0), horizontalPath.Location);

            // Vertical axis
            Assert.AreEqual(verticalPath.First().Direction, Direction.Vertical);
            Assert.AreEqual(4, verticalPath.First().Length);
            Assert.AreEqual(new Point(4, 4), verticalPath.Location);
        }
    }
}
