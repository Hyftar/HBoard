using System.Drawing;
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
    public class BishopTestUnit
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

            var bishop = gameContext.Board
                .Select((x, i) => new { Index = i, Cell = x == null ? null : x.Content as BishopUnit})
                .First(x => x.Cell != null);

            var path = bishop.Cell.GetMovementPaths(gameContext.Board, new Point(bishop.Index / 8, bishop.Index % 8)).First();

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

                    board.AddUnit<BishopUnit>(Orientation.Vertical, white, Point.Empty, new Point(3, 4));
                    board.AddUnit<RookUnit>(Orientation.Vertical, white, Point.Empty, new Point(1, 2));
                    board.AddUnit<PawnUnit>(Orientation.Vertical, black, Point.Empty, new Point(5, 1));
                },
                BoardSize = new Size(8, 8)
            };

            var gameContext = new GameContext(options, new[]
            {
                new ChessPlayer(PlayerType.White),
                new ChessPlayer(PlayerType.Black)
            });
            gameContext.Init();

            var bishop = gameContext.Board
                .Select((x, i) => new { Index = i, Cell = x == null ? null : x.Content as BishopUnit })
                .First(x => x.Cell != null);

            var paths = bishop.Cell.GetMovementPaths(gameContext.Board, new Point(bishop.Index / 8, bishop.Index % 8)).ToArray();
            MovementPath primePath = paths.First(), lastPath = paths.Last();

            // Prime axis
            Assert.AreEqual(primePath.First().Direction, AxisDirection.PrimeDiagonal);
            Assert.AreEqual(3, primePath.First().Length);
            Assert.AreEqual(new Point(2, 3), primePath.Location);

            // Last axis
            Assert.AreEqual(lastPath.First().Direction, AxisDirection.LastDiagonal);
            Assert.AreEqual(7, lastPath.First().Length);
            Assert.AreEqual(new Point(0, 7), lastPath.Location);
        }
    }
}
