using System;
using System.Drawing;
using System.Linq;
using ExtensionLib;
using HBoard.Chess.Units;
using HBoard.Core;

namespace HBoard.Chess
{
    public static class ChessLogic
    {
        public static IPlayer GetWinner(this GameContext game)
        {
            if (game == null)
                throw new ArgumentNullException("game");

            var players = game.Players.ToArray();
            if (players.Count(x => !x.Eliminated) == 1)
                return players.Single();

            return null;
        }

        public static Boolean HasWon(this GameContext game, IPlayer player)
        {
            if (player == null)
                throw new ArgumentNullException("player");

            return GetWinner(game) == player;
        }

        public static Boolean IsDraw(this GameContext game)
        {
            if (game == null)
                throw new ArgumentNullException("game");

            ChessOptions options = game.Options as ChessOptions;

            if (game.Board.Where(x => x != null && x.Content != null).Count(x => !(x.Content is KingUnit)) == 0)
                return true;

            if (options == null)
                return false;

            return false;
        }

        public static void ResetMovementContext(GameContext game)
        {
            if (game == null)
                throw new ArgumentNullException("game");

            var enumerator = game.Board.Cells.GetArrayEnumerator();

            while (enumerator.MoveNext())
            {
                var unit = ((BoardCell) enumerator.Current).Content as ChessUnit;

                if (unit != null)
                    unit.GenerateCache(game.Board, new Point(enumerator.Positions[0], enumerator.Positions[1]));
            }
        }
    }
}
