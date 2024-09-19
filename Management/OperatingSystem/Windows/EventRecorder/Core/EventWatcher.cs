using Microsoft.Extensions.FileSystemGlobbing;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace InfinityOfficialNetwork.Management.OperatingSystem.Windows.EventRecorder.Core
{
    [SupportedOSPlatform("windows")]
    internal class EventWatcher
    {
        EventLogSession? session;
        List<string> logNames;
        EventHandler<EventRecordWrittenEventArgs> eventHandler;

        List<EventLogQuery> eventQuerys;
        List<EventLogWatcher> watchers;

        readonly ILogger<Worker> logger;

        internal EventWatcher(EventHandler<EventRecordWrittenEventArgs> eventHandler, ILogger<Worker> logger)
        {
            this.eventHandler = eventHandler;
            eventQuerys = new();
            watchers = new();
            this.logger = logger;

            logger.LogDebug("Event watcher created");
        }

        internal async Task Attach()
        {
            logger.LogInformation("Event watcher attaching to event logs");
            session = new EventLogSession();
            logNames = new List<string>(session.GetLogNames());

            foreach (var logName in logNames) try
                {
                    logger.LogInformation($"Event watcher attaching to event log {logName}");

                    string query = "*"; // '*' means all events
                    EventLogQuery eventQuery = new EventLogQuery(logName, PathType.LogName, query);
                    eventQuerys.Add(eventQuery);

                    EventLogWatcher watcher = new EventLogWatcher(eventQuery);
                    watchers.Add(watcher);

                    watcher.EventRecordWritten += eventHandler;
                    watcher.Enabled = true;
                }
                catch (Exception ex)
                {
                    logger.LogCritical(ex, $"An error occured while attaching to log {logName}");
                    throw new Exception("Failed to attach to event log", ex);
                }
        }

        internal async Task Reattach()
        {
            logger.LogInformation($"Event watcher reattaching");

            Dettach();
            Attach();
        }

        internal async Task Dettach()
        {
            logger.LogInformation($"Event watcher detaching");
            foreach (var watcher in watchers)
            {
                watcher.Enabled = false;
                watcher.EventRecordWritten -= eventHandler;
            }

            watchers.Clear();
            eventQuerys.Clear();
            logNames.Clear();
        }
    }
}
