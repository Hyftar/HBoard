using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void TestMethod2()
        {
            var options = new GameOptions
            {
                PopulateBoardDelegate = delegate(GameContext context, GameBoard board)
                {
                    ChessPlayer white = (ChessPlayer) context.Players.First(),
                                black = (ChessPlayer) context.Players.Last();

                    board.AddUnit<KingUnit>(white, Point.Empty, new Point(4, 0));
                    board.AddUnit<KingUnit>(black, Point.Empty, new Point(4, 0));
                },
                BoardSize = new Size(8, 8)
            };

            var gameContext = new GameContext(options, new[]
            {
                new ChessPlayer(PlayerType.White),
                new ChessPlayer(PlayerType.Black)
            });
            gameContext.Init();

            var enumerator = new IndexableEnumerator<BoardCell>(gameContext.Board.GetEnumerator(), gameContext.Board.Cells);
            while (enumerator.MoveNext())
            {
                Debug.WriteLine(enumerator.Index + ": " + (enumerator.Current == null ? "null" : enumerator.Positions.JoinFormat(", ", "[{0}]", enumerator.Current)));
            }


            Assert.IsTrue(gameContext.IsDraw());
        }
    }
}
