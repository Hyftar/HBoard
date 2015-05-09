using System;

namespace HBoard.Core
{
    /// <summary>
    /// Represents a game structure with a specific configuration.
    /// </summary>
    public class GameContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:HBoard.Core.GameContext"/> class.
        /// </summary>
        /// <param name="options">The object to use to provide options for this <see cref="T:HBoard.Core.GameContext"/>.</param>
        /// <param name="players">The array of players.</param>
        public GameContext(GameOptions options, IPlayer[] players)
        {
            this.Options = options;
            this.Players = players;
        }

        /// <summary>
        /// Gets the game options used to customize the board interaction.
        /// </summary>
        public GameOptions Options { get; private set; }

        /// <summary>
        /// Gets the board that holds the cells.
        /// </summary>
        public GameBoard Board { get; private set; }

        /// <summary>
        /// Gets the players registered to this game.
        /// </summary>
        public IPlayer[] Players { get; private set; }

        /// <summary>
        /// Gets a boolean value indicating whether the current game has been initialized.
        /// </summary>
        public Boolean Initialized { get; private set; }

        /// <summary>
        /// Initializes the current <see cref="T:HBoard.Core.GameContext"/>.
        /// </summary>
        public void Init()
        {
            if (this.Options == null)
                return;

            if (this.Options.PopulateBoardDelegate == null)
                return;

            if (this.Initialized)
                return;

            this.Initialized = true;
            this.Board = new GameBoard(this.Options.BoardSize);

            // Initialize/populate the using the provided delegate.
            this.Options.PopulateBoardDelegate(this, this.Board);
        }

        /// <summary>
        /// Resets the state of the current <see cref="T:HBoard.Core.GameContext"/>, allowing the object to be reused.
        /// </summary>
        public void Reset()
        {
            this.Initialized = false;
            this.Board = null;
        }
    }
}
