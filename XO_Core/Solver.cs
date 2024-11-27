using System.Collections.Generic;
using Timer = System.Timers.Timer;

namespace XO_Core
{
    public class Solver<Move, State> where Move : struct
    {
        public static Move IterativeDeepeningAlfaBetaSearch(IGame<Move, State> game, State state, Timer timer)
        {
            Player onTurn = game.OnTurn(state);
            bool timerElapsed = false;
            timer.Elapsed += (_, _) => timerElapsed = true;
            timer.Start();
            for (int depth = 0; ; depth++)
            {
                MoveEval<Move> best;
                if (onTurn == Player.x)
                    best = MaxSearch(game, state, int.MinValue, int.MaxValue, 0, depth, timer);
                else
                    best = MinSearch(game, state, int.MinValue, int.MaxValue, 0, depth, timer);

                if (game.Win(best.eval) || timerElapsed)
                {
                    if (best.move == null)
                        throw new Exception("No move");
                    return best.move.Value;
                }
            }
        }

        private static MoveEval<Move> MaxSearch(IGame<Move, State> game, State state, int alfa, int beta, int depth, int maxDepth, Timer timer)
        {
            if (game.IsCutoff(state, depth, maxDepth, timer))
                return MaxQuiescenceSearch(game, state);

            var best = MoveEval<Move>.Min;
            foreach (var move in game.GetMoves(state))
            {
                State newState = game.ApplyMove(move);
                var newMove = MinSearch(game, newState, alfa, beta, depth++, maxDepth, timer);
                if (newMove.eval > best.eval)
                {
                    best = newMove;
                    alfa = Math.Max(alfa, best.eval);
                }
                if (best.eval >= beta)
                    return best;
            }
            return best;
        }

        private static MoveEval<Move> MinSearch(IGame<Move, State> game, State state, int alfa, int beta, int depth, int maxDepth, Timer timer)
        {
            if (game.IsCutoff(state, depth, maxDepth, timer))
                return MinQuiescenceSearch(game, state);

            var best = MoveEval<Move>.Max;
            foreach (var move in game.GetMoves(state))
            {
                State newState = game.ApplyMove(move);
                var newMove = MaxSearch(game, newState, alfa, beta, depth++, maxDepth, timer);
                if (newMove.eval < best.eval)
                {
                    best = newMove;
                    beta = Math.Min(alfa, best.eval);
                }
                if (best.eval <= alfa)
                    return best;
            }
            return best;
        }

        private static MoveEval<Move> MaxQuiescenceSearch(IGame<Move, State> game, State state)
        {
            var moves = game.GetQuiescenceMoves(state);
            if (moves.Count == 0)
                return new MoveEval<Move>(game.Eval(state));

            var best = MoveEval<Move>.Min;
            foreach (var move in moves)
            {
                State newState = game.ApplyMove(move);
                var newMove = MinQuiescenceSearch(game, newState);
                if (best.eval > newMove.eval)
                    best = newMove;
            }
            return best;
        }

        private static MoveEval<Move> MinQuiescenceSearch(IGame<Move, State> game, State state)
        {
            var moves = game.GetQuiescenceMoves(state);
            if (moves.Count == 0)
                return new MoveEval<Move>(game.Eval(state));

            var best = MoveEval<Move>.Max;
            foreach (var move in moves)
            {
                State newState = game.ApplyMove(move);
                var newMove = MaxQuiescenceSearch(game, newState);
                if (best.eval < newMove.eval)
                    best = newMove;
            }
            return best;
        }
    }
}
