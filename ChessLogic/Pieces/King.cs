namespace ChessLogic.Pieces
{
    public class King : Piece
    {
        public override PieceType Type => PieceType.King;
        public override Player Color { get; }
        private Direction[] dirs = new Direction[]
        {
            Direction.North,
            Direction.South,
            Direction.East,
            Direction.West,
            Direction.NorthEast,
            Direction.SouthEast,
            Direction.NorthWest,
            Direction.SouthWest
        };
        public King(Player color) 
        {
            Color = color;
        }
        public override Piece Copy()
        {
            King copy = new King(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            foreach (Direction dir in dirs)
            {
                Position to = from + dir;
                if (board.IsInside(to) && (board.IsEmpty(to) || board[to].Color != Color))
                {
                    yield return new NormalMove(from, to);
                }
            }
        }
    }
}
