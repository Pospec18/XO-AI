namespace XO.Core
{
    public interface IGame<Move, State>
    {
        /// <summary>
        /// Gets player on turn based on game state.
        /// </summary>
        /// <param name="state">Current game state</param>
        /// <returns>Player on turn in this game state</returns>
        public Player OnTurn(State state);

        /// <summary>
        /// Get all valid moves for given game state.
        /// When moves are ordered by best to worst, solver runs faster.
        /// </summary>
        /// <param name="state">Current game state</param>
        /// <returns>Ordered list of moves, from best to worst</returns>
        public IList<Move> GetMoves(State state);

        /// <summary>
        /// Get only game changing moves for given game state.
        /// Moves that if left unchecked can decide the game.
        /// Like king check in chess or unbroken line of 3 symbols in gomoku.
        /// </summary>
        /// <param name="state">Current game state</param>
        /// <returns>Ordered list of moves, from best to worst</returns>
        public IList<Move> GetQuiescenceMoves(State state);

        /// <summary>
        /// Apply move to current game state
        /// </summary>
        /// <param name="state">Current game state</param>
        /// <param name="move">Move to apply</param>
        /// <returns>New state with applied move</returns>
        public State ApplyMove(State state, Move move);

        /// <summary>
        /// Returns if game ends in this state.
        /// </summary>
        /// <param name="state">Current game state</param>
        /// <returns>Does game ended</returns>
        public bool GameOver(State state);

        /// <summary>
        /// Evaluate state with int value.
        /// Positive values are better for player X, negative for player O.
        /// Better positions have greater values for goven player.
        /// Win for player has highest value (with respect of player's sign).
        /// </summary>
        /// <param name="state">Current game state</param>
        /// <returns>Evaluation of state</returns>
        public int Eval(State state);

        /// <summary>
        /// Does evaluation correspond to some winning position.
        /// </summary>
        /// <param name="eval">Evaluation of some board</param>
        /// <returns>Someone wins</returns>
        public bool Win(int eval);
    }

    public enum Player { x = 1, o = -1, none = 0 }
}
