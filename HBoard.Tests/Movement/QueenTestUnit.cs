﻿using System.Drawing;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HBoard.Core;
using HBoard.Chess;
using HBoard.Chess.Generation;
using HBoard.Chess.Units;
using HBoard.Logic;

namespace HBoard.Tests.Movement
{
    [TestClass]
    public class QueenTestUnit
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

            var queen = gameContext.Board
                .Select((x, i) => new { Index = i, Cell = x == null ? null : x.Content as QueenUnit })
                .First(x => x.Cell != null);

            var path = queen.Cell.GetMovementPaths(gameContext.Board, new Point(queen.Index / 8, queen.Index % 8)).First().ToArray();

            Assert.AreEqual(0, path.First().Length);
            Assert.AreEqual(0, path.Last().Length);
        }

        [TestMethod]
        public void TestLastAxisIntersectOrigin()
        {
            var options = new GameOptions
            {
                PopulateBoardDelegate = delegate(GameContext context, GameBoard board)
                {
                    var white = (ChessPlayer) context.Players.First(),
                        black = (ChessPlayer) context.Players.Last();

                    board.AddUnit<RookUnit>(Orientation.Vertical, white, Point.Empty, new Point(3, 4));
                    board.AddUnit<BishopUnit>(Orientation.Vertical, white, Point.Empty, new Point(1, 4));
                    board.AddUnit<PawnUnit>(Orientation.Vertical, black, Point.Empty, new Point(3, 1));
                    board.AddUnit<QueenUnit>(Orientation.Vertical, white, Point.Empty, new Point(1, 6));
                },
                BoardSize = new Size(8, 8)
            };

            var gameContext = new GameContext(options, new[]
            {
                new ChessPlayer(PlayerType.White),
                new ChessPlayer(PlayerType.Black)
            });
            gameContext.Init();

            var queen = gameContext.Board
                .Select((x, i) => new { Index = i, Cell = x == null ? null : x.Content as QueenUnit })
                .First(x => x.Cell != null);

            var paths = queen.Cell.GetMovementPaths(gameContext.Board, new Point(queen.Index / 8, queen.Index % 8)).ToArray();
            MovementPath primePath = paths[0], lastPath = paths[1],
                         horizontalPath = paths[2], verticalPath = paths[3];

            // Prime axis
            Assert.AreEqual(primePath.First().Direction, AxisDirection.PrimeDiagonal);
            Assert.AreEqual(2, primePath.First().Length);
            Assert.AreEqual(new Point(0, 5), primePath.Location);

            // Last axis
            Assert.AreEqual(lastPath.First().Direction, AxisDirection.LastDiagonal);
            Assert.AreEqual(2, lastPath.First().Length);
            Assert.AreEqual(new Point(0, 7), lastPath.Location);

            // Horizontal axis
            Assert.AreEqual(horizontalPath.First().Direction, AxisDirection.Horizontal);
            Assert.AreEqual(2, horizontalPath.First().Length);
            Assert.AreEqual(new Point(1, 5), horizontalPath.Location);

            // Vertical axis
            Assert.AreEqual(verticalPath.First().Direction, AxisDirection.Vertical);
            Assert.AreEqual(3, verticalPath.First().Length);
            Assert.AreEqual(new Point(0, 6), verticalPath.Location);
        }
    }
}
