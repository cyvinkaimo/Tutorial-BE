namespace Yousource.Infrastructure.Exceptions
{
    using System;

    public class TutorialServiceException : RethrowableException
    {
        public TutorialServiceException(Exception innerException, string message)
            : base(innerException, message)
        {
        }

        public TutorialServiceException(Exception innerException)
            : base(innerException, string.Empty)
        {
        }
    }
}
