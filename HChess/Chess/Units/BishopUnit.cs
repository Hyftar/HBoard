using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using HBoard.Core;
using HBoard.Logic;
using HBoard.Chess.Logic;
using ExtensionLib;

namespace HBoard.Chess.Units
{
    public class BishopUnit : ChessUnit
    {
        public BishopUnit() : base() { }

        public BishopUnit(IPlayer player)
            : base(player) { }

        /// <summary>
        /// Determines whether the chess unit can be moved to a specific cell.
        /// </summary>
        /// <param name="origin">The location of the cell holding the unit.</param>
        /// <param name="target">The location of the targetted cell.</param>
        /// <returns>A boolean value indicating whether the unit is able to move to a given cell.</returns>
        public override Boolean CanMove(GameBoard board, Point origin, Point target)
        {
            BoardCell targetCell = board.Cells[target.X, target.Y];

            if (targetCell.Content.Player == this.Player)
                return false;

            return true;
        }

        // Glossary:
        // O: Origin (zero-adjusted; represents System.Point)
        // T: Target (coincides with the metric)
        // X: BoardCell

        // Full path representation:
            // O -> Point(x,y)
            // X
            // X T

        // First vector (w/ MovementType.None):
            // O
            // X
            // X -> Metric (2)

        // Second vector (w/ MovementType.Last):
            // O T -> Metric (1)

        public override IEnumerable<MovementPath> GetMovementPaths(GameBoard board, Point position)
        {
            // Generate the array with 2 slots, as there are only two axes.
            MovementPath[] paths = new MovementPath[2];

            // To be computed using the passed coordinates (i.e. 'position').
            Point primeLocation = Point.Empty;
            Point lastLocation = Point.Empty;

            // To be computed (i.e. the amount of cells between the origin and the target position).
            int primeMetric = 0;
            int lastMetric = 0;
            int primeOriginAdjustment = position.X - position.Y;
            int lastOriginAdjustment = position.X + position.Y;
            bool hasCrossedOrigin = false;
            bool primeMetricEnded = false;
            bool lastMetricEnded = false;

            var enumerator = new ArrayEnumerator(board.ToArray());
            while (enumerator.MoveNext())
            {
                BoardCell cell = (BoardCell) enumerator.Current;
                bool isOnPrimeAxis = enumerator.Positions[0] == enumerator.Positions[1] + primeOriginAdjustment;
                bool isOnLastAxis = enumerator.Positions[0] == lastOriginAdjustment - enumerator.Positions[1];

                if (enumerator.Positions[0] == position.X && enumerator.Positions[1] == position.Y)
                {
                    hasCrossedOrigin = true;
                    continue;
                }

                if (primeMetricEnded && board[enumerator.Positions[0], enumerator.Positions[1]].Content != null && isOnPrimeAxis && !hasCrossedOrigin)
                {
                    if (board[enumerator.Positions[0], enumerator.Positions[1]].Content.Player == this.Player)
                    {
                        primeLocation = new Point(enumerator.Positions[0] + 1, enumerator.Positions[1] + 1);
                        primeMetric = 0;
                        continue;
                    }
                    else if (board[enumerator.Positions[0],enumerator.Positions[1]].Content.Player != this.Player)
                    {
                        primeLocation = new Point(enumerator.Positions[0], enumerator.Positions[1]);
                        primeMetric = 1;
                        continue;
                    }

                    primeMetric++;
                }

                else if (lastMetricEnded && board[enumerator.Positions[0], enumerator.Positions[1]].Content != null && isOnLastAxis && !hasCrossedOrigin)
                {
                    if (board[enumerator.Positions[0], enumerator.Positions[1]].Content.Player == this.Player)
                    {
                        lastLocation = new Point(enumerator.Positions[0] + 1, enumerator.Positions[1] + 1);
                        lastMetric = 0;
                        continue;
                    }
                    else if (board[enumerator.Positions[0], enumerator.Positions[1]].Content.Player != this.Player)
                    {
                        lastLocation = new Point(enumerator.Positions[0], enumerator.Positions[1]);
                        lastMetric = 1;
                        continue;
                    }
                    else if (board[enumerator.Positions[0], enumerator.Positions[1]].Content != null && board[enumerator.Positions[0], enumerator.Positions[1]].Content.Player == this.Player && hasCrossedOrigin)
                    {
                        lastMetricEnded = true;
                        continue;
                    }
                    lastMetric++;
                }
            }

            paths[0] = this.GetPath(primeLocation, primeMetric);
            paths[1] = this.GetPath(lastLocation, lastMetric);
            return paths;
        }
            
        protected MovementPath GetPath(Point location, Int64 metric)
        {
            return new MovementPath(
                location: location,
                vectors: new SpecializedMovementVector(
                    direction: Direction.Diagonal,
                    metric: metric,
                    movement: MovementType.All));
        }
    }
}
