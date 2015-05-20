using System;
using System.Drawing;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExtensionLib;
using HBoard.Core;
using HBoard.Chess;
using HBoard.Chess.Generation;

namespace HBoard.Tests
{

    [TestClass]
    public class GenerationTestUnit
    {
        [TestMethod]
        public void TestClassicBoard()
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
            const String path = @"..\..\GeneratedBoards\ChessBoard.html";
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                File.Create(path).Close();
            }
            catch { }
            using (StreamWriter sw = new StreamWriter(path, false) { AutoFlush = true })
            {
                sw.Write(@"
<head>
<style type=""text/css"">
    table {
        border: 10px solid #665229;
        }
    td, th {
        width: 100px;
        height: 100px;
        text-align: center;
        border: 2px solid black;
    }
    .white {
        background-color: #FFCC66;
        color: black;
    }
    .black {
        background-color: #665229;
        color: black;
    }
</style>
</head>
<body>
<table id=""chess_board"" cellpadding=""0"" cellspacing=""0"" style=""background-color: #FFDB94"">
<caption style= ""font-size: 25"">HBoard Chess<caption>
<tr style= ""width: 50px"">
<th></th>
<th>A</th>
<th>B</th>
<th>C</th>
<th>D</th>
<th>E</th>
<th>F</th>
<th>G</th>
<th>H</th>
</tr>");
                var enumerator = gameContext.Board.Cells.GetArrayEnumerator();
                int y = 0;

                sw.WriteLine("</tr><tr><th>" + (gameContext.Board.Height - y) + "</th>");

                while (enumerator.MoveNext())
                {
                    if (y != enumerator.Positions[0])
                    {
                        y = enumerator.Positions[0];
                        sw.WriteLine("</tr><tr><th>" + (gameContext.Board.Height - y) +"</th>");
                        sw.Flush();
                    }
                    var cell = (BoardCell) enumerator.Current;
                    var player = cell == null || cell.Content == null
                        ? null
                        : (ChessPlayer) cell.Content.Player;
                    sw.WriteLine(
                        "<td style=\"" +
                        (player == null
                            ? null
                            : "background-color: " + (player.Type == PlayerType.Black ? "#444" : "#ccc")) + "\">" +
                        (enumerator.Current == null ? null : enumerator.Current.ToString()) + "</td>");
                    sw.Flush();
                }
                sw.WriteLine("</table></body>");
            }
        }
    }
}
