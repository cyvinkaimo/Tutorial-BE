namespace Yousource.Infrastructure.Logging
{
    using System;
    using System.Collections.Generic;

    public interface ILogger
    {
        void WriteException(Exception e, IDictionary<string, string> properties = null);

        void TrackEvent(string eventName, IDictionary<string, string> properties = null);
    }
}
