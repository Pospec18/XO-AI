using Timer = System.Timers.Timer;

namespace XO_Core
{
    public interface IGame<Move, State>
    {
        public Player OnTurn(State state);
        public List<Move> GetMoves(State state);
        public List<Move> GetQuiescenceMoves(State state);
        public State ApplyMove(Move move);
        public bool IsCutoff(State state, int depth, int maxDepth, Timer timer);
        public int Eval(State state);
        public bool Win(int eval);
    }

    public enum Player { x = 1, o = -1, none = 0 }
}
