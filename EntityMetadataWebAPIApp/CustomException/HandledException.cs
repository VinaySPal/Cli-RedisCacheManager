using System;

namespace CustomException
{
     /// <summary>
     /// It is used to raise custom exception to the user.
     /// </summary>
    public class HandledException : Exception
    {
        public HandledException()
            : base()
        { }

        public HandledException(string message)
            : base(message)
        { }

        public HandledException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
