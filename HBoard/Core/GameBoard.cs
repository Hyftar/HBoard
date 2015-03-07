using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace HBoard.Core
{
    public class GameBoard : IEnumerable<BoardCell>
    {
        private const String INVALID_DIMENSION = "The provided dimension is invalid; the board size cannot be of 0 cells or fewer.";

        internal GameBoard(Int32 size)
        {
            if (size <= 0)
                throw new ArgumentException("size", INVALID_DIMENSION);

            this.Size = new Size(size, size);
            this.InitializeBoard();
        }

        internal GameBoard(Int32 width, Int32 height)
        {
            if (width <= 0)
                throw new ArgumentException("width", INVALID_DIMENSION);

            if (height <= 0)
                throw new ArgumentException("height", INVALID_DIMENSION);

            this.Size = new Size(width, height);
            this.InitializeBoard();
        }

        internal GameBoard(Size size)
        {
            if (size.Width <= 0 || size.Height <= 0)
                throw new ArgumentException("size", INVALID_DIMENSION);
            
            this.Size = size;
            this.InitializeBoard();
        }

        protected void InitializeBoard()
        {
            this.Cells = new BoardCell[this.Size.Width, this.Size.Height];
        }

        public Size Size { get; private set; }

        public Int32 Width { get { return this.Size.Width; } }

        public Int32 Height { get { return this.Size.Height; } }

        public BoardCell[,] Cells { get; private set; }

        public BoardCell this[Int32 x, Int32 y]
	    {
		    get { return this.GetCell(x, y); }
            set { this.Cells[x, y] = value; }
	    }

        public BoardCell this[Point point]
        {
            get { return this.GetCell(point); }
            set { this.Cells[point.X, point.Y] = value; }
        }

        public BoardCell GetCell(Int32 x, Int32 y)
        {
            if (this.Cells[x, y] == null)
                return this.Cells[x, y] = new BoardCell();

            return this.Cells[x, y];
        }

        public BoardCell GetCell(Point point)
        {
            return this.GetCell(point.X, point.Y);
        }

        public IEnumerator<BoardCell> GetEnumerator()
        {
            return this.Cells.Cast<BoardCell>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Cells.GetEnumerator();
        }
    }
}
