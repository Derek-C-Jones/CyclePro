using System;

namespace CyclePro.Core.Exceptions
{
    class InvalidInputFileException : Exception
    {
        private static string defaultMessage = "The input file that you specified is invalid.";
        public InvalidInputFileException()
            : base(defaultMessage)
        {
        }

        public InvalidInputFileException(string message) 
            : base (message)
        {
        }

        public InvalidInputFileException(Exception innerException)
           : base(defaultMessage, innerException)
        {
        }

        public InvalidInputFileException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
