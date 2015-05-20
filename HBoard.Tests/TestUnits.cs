using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.IO;
using HBoard.Core;
using HBoard.Chess;
using HBoard.Chess.Units;
using HBoard.Chess.Generation;

namespace HBoard.Tests
{

    [TestClass]
    public class TestUnits
    {
        [TestMethod]
        public void TestMethod1()
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

            using (StreamWriter sw = new StreamWriter(@"C:\Users\Utilisateur\Documents\boop\ChessBoard.html", false))
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
        <th>N/A</th>
        <th>A</th>
        <th>B</th>
        <th>C</th>
        <th>D</th>
        <th>E</th>
        <th>F</th>
        <th>G</th>
        <th>H</th>
    </tr>
    <tr>
        <th>8</th>
        <td id=""A8"" class=""white"">" + gameContext.Board[0, 7].ToString() + @"</td>
        <td id=""B8"" class=""black"">" + gameContext.Board[1, 7].ToString() + @"</td>
        <td id=""C8"" class=""white"">" + gameContext.Board[2, 7].ToString() + @"</td>
        <td id=""D8"" class=""black"">" + gameContext.Board[3, 7].ToString() + @"</td>
        <td id=""E8"" class=""white"">" + gameContext.Board[4, 7].ToString() + @"</td>
        <td id=""F8"" class=""black"">" + gameContext.Board[5, 7].ToString() + @"</td>
        <td id=""G8"" class=""white"">" + gameContext.Board[6, 7].ToString() + @"</td>
        <td id=""H8"" class=""black"">" + gameContext.Board[7, 7].ToString() + @"</td>
    </tr>
    <tr>
        <th>7</th>
        <td id=""A7"" class=""black"">" + gameContext.Board[0, 6].ToString() + @"</td>
        <td id=""B7"" class=""white"">" + gameContext.Board[1, 6].ToString() + @"</td>
        <td id=""C7"" class=""black"">" + gameContext.Board[2, 6].ToString() + @"</td>
        <td id=""D7"" class=""white"">" + gameContext.Board[3, 6].ToString() + @"</td>
        <td id=""E7"" class=""black"">" + gameContext.Board[4, 6].ToString() + @"</td>
        <td id=""F7"" class=""white"">" + gameContext.Board[5, 6].ToString() + @"</td>
        <td id=""G7"" class=""black"">" + gameContext.Board[6, 6].ToString() + @"</td>
        <td id=""H7"" class=""white"">" + gameContext.Board[7, 6].ToString() + @"</td>
    </tr>
    <tr>
        <th>6</th>
        <td id=""A6"" class=""white"">" + gameContext.Board[0, 5].ToString() + @"</td>
        <td id=""B6"" class=""black"">" + gameContext.Board[1, 5].ToString() + @"</td>
        <td id=""C6"" class=""white"">" + gameContext.Board[2, 5].ToString() + @"</td>
        <td id=""D6"" class=""black"">" + gameContext.Board[3, 5].ToString() + @"</td>
        <td id=""E6"" class=""white"">" + gameContext.Board[4, 5].ToString() + @"</td>
        <td id=""F6"" class=""black"">" + gameContext.Board[5, 5].ToString() + @"</td>
        <td id=""G6"" class=""white"">" + gameContext.Board[6, 5].ToString() + @"</td>
        <td id=""H6"" class=""black"">" + gameContext.Board[7, 5].ToString() + @"</td>
    </tr>
    <tr>
        <th>5</th>
        <td id=""A5"" class=""black"">" + gameContext.Board[0, 4].ToString() + @"</td>
        <td id=""B5"" class=""white"">" + gameContext.Board[1, 4].ToString() + @"</td>
        <td id=""C5"" class=""black"">" + gameContext.Board[2, 4].ToString() + @"</td>
        <td id=""D5"" class=""white"">" + gameContext.Board[3, 4].ToString() + @"</td>
        <td id=""E5"" class=""black"">" + gameContext.Board[4, 4].ToString() + @"</td>
        <td id=""F5"" class=""white"">" + gameContext.Board[5, 4].ToString() + @"</td>
        <td id=""G5"" class=""black"">" + gameContext.Board[6, 4].ToString() + @"</td>
        <td id=""H5"" class=""white"">" + gameContext.Board[7, 4].ToString() + @"</td>
    </tr>
    <tr>
        <th>4</th>
        <td id=""B4"" class=""white"">" + gameContext.Board[0, 3].ToString() + @"</td>
        <td id=""A4"" class=""black"">" + gameContext.Board[1, 3].ToString() + @"</td>
        <td id=""C4"" class=""white"">" + gameContext.Board[2, 3].ToString() + @"</td>
        <td id=""D4"" class=""black"">" + gameContext.Board[3, 3].ToString() + @"</td>
        <td id=""E4"" class=""white"">" + gameContext.Board[4, 3].ToString() + @"</td>
        <td id=""F4"" class=""black"">" + gameContext.Board[5, 3].ToString() + @"</td>
        <td id=""G4"" class=""white"">" + gameContext.Board[6, 3].ToString() + @"</td>
        <td id=""H4"" class=""black"">" + gameContext.Board[7, 3].ToString() + @"</td>
    </tr>
    <tr>
        <th>3</th>
        <td id=""A3"" class=""black"">" + gameContext.Board[0, 2].ToString() + @"</td>
        <td id=""B3"" class=""white"">" + gameContext.Board[1, 2].ToString() + @"</td>
        <td id=""C3"" class=""black"">" + gameContext.Board[2, 2].ToString() + @"</td>
        <td id=""D3"" class=""white"">" + gameContext.Board[3, 2].ToString() + @"</td>
        <td id=""E3"" class=""black"">" + gameContext.Board[4, 2].ToString() + @"</td>
        <td id=""F3"" class=""white"">" + gameContext.Board[5, 2].ToString() + @"</td>
        <td id=""G3"" class=""black"">" + gameContext.Board[6, 2].ToString() + @"</td>
        <td id=""H3"" class=""white"">" + gameContext.Board[7, 2].ToString() + @"</td>
    </tr>
    <tr>
        <th>2</th>
        <td id=""A2"" class=""white"">" + gameContext.Board[0, 1].ToString() + @"</td>
        <td id=""B2"" class=""black"">" + gameContext.Board[1, 1].ToString() + @"</td>
        <td id=""C2"" class=""white"">" + gameContext.Board[2, 1].ToString() + @"</td>
        <td id=""D2"" class=""black"">" + gameContext.Board[3, 1].ToString() + @"</td>
        <td id=""E2"" class=""white"">" + gameContext.Board[4, 1].ToString() + @"</td>
        <td id=""F2"" class=""black"">" + gameContext.Board[5, 1].ToString() + @"</td>
        <td id=""G2"" class=""white"">" + gameContext.Board[6, 1].ToString() + @"</td>
        <td id=""H2"" class=""black"">" + gameContext.Board[7, 1].ToString() + @"</td>
    </tr>
    <tr>
        <th>1</th>
        <td id=""A1"" class=""black"">" + gameContext.Board[0, 0].ToString() + @"</td>
        <td id=""B1"" class=""white"">" + gameContext.Board[1, 0].ToString() + @"</td>
        <td id=""C1"" class=""black"">" + gameContext.Board[2, 0].ToString() + @"</td>
        <td id=""D1"" class=""white"">" + gameContext.Board[3, 0].ToString() + @"</td>
        <td id=""E1"" class=""black"">" + gameContext.Board[4, 0].ToString() + @"</td>
        <td id=""F1"" class=""white"">" + gameContext.Board[5, 0].ToString() + @"</td>
        <td id=""G1"" class=""black"">" + gameContext.Board[6, 0].ToString() + @"</td>
        <td id=""H1"" class=""white"">" + gameContext.Board[7, 0].ToString() + @"</td>
    </tr>
</table>
</body>");
            }
        }
    }
}
