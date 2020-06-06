namespace Yousource.Infrastructure.Logging
{
    using System;
    using Microsoft.ApplicationInsights;
    using Microsoft.ApplicationInsights.Extensibility;

    public class InsightsLogger : ILogger
    {
        private readonly TelemetryClient client;

        public InsightsLogger(string instrumentationKey)
        {
            this.client = new TelemetryClient(new TelemetryConfiguration(instrumentationKey));
        }

        public void WriteException(Exception e)
        {
            this.client.TrackException(e);
        }
    }
}
