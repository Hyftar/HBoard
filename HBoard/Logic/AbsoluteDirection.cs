namespace HBoard.Logic
{
    public struct AbsoluteDirection
    {
        public AbsoluteDirection(Direction direction, Orientation orientation)
            : this()
        {
            this.Direction = direction;
            this.Orientation = orientation;
        }

        public Direction Direction { get; private set; }

        public Orientation Orientation { get; private set; }
    }
}
