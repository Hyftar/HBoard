using System;

namespace HBoard.Chess.Generation
{
    public class BoardGenerationException : Exception
    {
        public BoardGenerationException()
            : base("The board failed to generate.") { }

        public BoardGenerationException(String message)
            : base(message) { }

        public BoardGenerationException(String message, Exception innerException)
            : base(message, innerException) { }
    }
}
