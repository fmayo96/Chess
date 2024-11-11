namespace ChessLogic
{
    public class Pawn : Piece
    {
        public override PieceType Type => PieceType.Pawn;
        public override Player Color { get; }
        private readonly Direction forward;
        public Pawn(Player color) 
        {
            Color = color;
            if (color == Player.White)
            {
                forward = Direction.North;
            } else
            {
                forward = Direction.South;
            }
        }
        public override Piece Copy()
        {
            Pawn copy = new Pawn(Color);
            copy.HasMoved = HasMoved;
            return copy;
        }
        private bool CanMoveTo(Position pos, Board board)
        {
            return board.IsInside(pos) && board.IsEmpty(pos);
        }
        private bool CanCapture(Position pos, Board board)
        {
            if (board.IsEmpty(pos))
            {
                return false;
            }
            Piece piece = board[pos];
            if (piece.Color == Color)
            {
                return false;
            }
            return true;
        }
        private IEnumerable<Move> ForwardMoves(Position from, Board board)
        {
            Position oneMovePos = from + forward;
            if (CanMoveTo(oneMovePos, board))
            {
                yield return new NormalMove(from, oneMovePos);
                Position twoMovePos = oneMovePos + forward;
                if (!HasMoved)
                {
                    if (CanMoveTo(twoMovePos, board))
                    {
                        yield return new NormalMove(from, twoMovePos);
                    }
                }
            }
        }
        private IEnumerable<Move> DiagonalMoves(Position from, Board board)
        {
            foreach(Direction dir in new Direction[] { Direction.East, Direction.West})
            {
                Position to = from + forward + dir;
                if (CanCapture(to, board))
                {
                    yield return new NormalMove(from, to);
                }
            }
        }
        public override IEnumerable<Move> GetMoves(Position from, Board board)
        {
            return ForwardMoves(from, board).Concat(DiagonalMoves(from, board));
        }
    }
}
