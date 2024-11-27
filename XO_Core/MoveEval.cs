namespace XO.Core
{
    /// <summary>
    /// Struct for storing move with it's evaluation
    /// </summary>
    internal struct MoveEval<Move> where Move : struct
    {
        public Move? move;
        public int eval;

        public MoveEval(Move? move, int eval)
        {
            this.move = move;
            this.eval = eval;
        }

        public MoveEval(int eval)
        {
            move = null;
            this.eval = eval;
        }

        public static MoveEval<Move> Min = new MoveEval<Move>(null, int.MinValue);
        public static MoveEval<Move> Max = new MoveEval<Move>(null, int.MaxValue);
    }
}
