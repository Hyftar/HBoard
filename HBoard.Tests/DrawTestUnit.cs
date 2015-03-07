using System.Drawing;
using System.Diagnostics;
using System.Linq;
using HBoard.Core;
using HBoard.Chess;
using HBoard.Chess.Units;
using HBoard.Chess.Generation;
using ExtensionLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HBoard.Tests
{
    [TestClass]
    public class DrawTestUnit
    {
        [TestMethod]
        public void TestKings()
        {
            var options = new GameOptions
            {
                PopulateBoardDelegate = delegate(GameContext context, GameBoard board)
                {
                    ChessPlayer white = (ChessPlayer) context.Players.First(),
                                black = (ChessPlayer) context.Players.Last();

                    board.AddUnit<KingUnit>(Orientation.Vertical, white, Point.Empty, new Point(0, 4));
                    board.AddUnit<KingUnit>(Orientation.Vertical, black, Point.Empty, new Point(0, 4));
                },
                BoardSize = new Size(8, 8)
            };

            var gameContext = new GameContext(options, new[]
            {
                new ChessPlayer(PlayerType.White),
                new ChessPlayer(PlayerType.Black)
            });
            gameContext.Init();

            var enumerator = gameContext.Board.Cells.GetArrayEnumerator();
            while (enumerator.MoveNext())
            {
                Debug.WriteLine(enumerator.Index + ": " + (enumerator.Current == null ? "null" : enumerator.Positions.JoinFormat(", ", "[{0}]", enumerator.Current)));
            }

            Assert.IsTrue(gameContext.IsDraw());
        }
    }
}
