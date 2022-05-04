namespace Yousource.Infrastructure.Exceptions
{
    using System;

    public abstract class RethrowableException : Exception
    {
        public RethrowableException(Exception inner, string message = "")
            : base(message, inner)
        {
        }
    }
}
