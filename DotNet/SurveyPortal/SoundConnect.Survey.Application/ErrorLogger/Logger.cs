using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundConnect.Survey.Application.ErrorLogger
{
    public class Logger : ILogger
    {
        private const string EventSourceName = "SoundSurvey";
        private const string ApplicationLogName = "Application";

        public void Error(Exception e)
        {
            var msg = e.ToString();
            Log(msg, EventLogEntryType.Error);
        }

        public void Warning(Exception e)
        {
            var msg = e.ToString();
            Log(msg, EventLogEntryType.Warning);
        }

        private void Log(string errorMsg, EventLogEntryType entryType)
        {
            if (!EventLog.SourceExists(EventSourceName))
            {
                EventLog.CreateEventSource(EventSourceName, ApplicationLogName);
            }

            var log = new EventLog
            {
                Source = EventSourceName
            };

            log.WriteEntry(errorMsg, entryType);

            // pause for three seconds after logging before exiting? why?
            System.Threading.Thread.Sleep(3000);
        }
    }
}
