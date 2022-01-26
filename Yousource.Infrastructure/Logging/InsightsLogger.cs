namespace Yousource.Infrastructure.Logging
{
    using System;
    using System.Collections.Generic;
    using Microsoft.ApplicationInsights;
    using Microsoft.ApplicationInsights.Extensibility;

    public class InsightsLogger : ILogger
    {
        private readonly TelemetryClient client;

        public InsightsLogger(string instrumentationKey)
        {
            client = new TelemetryClient(new TelemetryConfiguration(instrumentationKey));
        }

        public void TrackEvent(string eventName, IDictionary<string, string> properties = null)
        {
            client.TrackEvent(eventName, properties);
        }

        public void WriteException(Exception e, IDictionary<string, string> properties = null)
        {
            client.TrackException(e, properties);
        }
    }
}
