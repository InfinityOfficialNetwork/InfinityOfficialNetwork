using Microsoft.Extensions.DependencyInjection;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace InfinityOfficialNetwork.Management.OperatingSystem.Windows.EventRecorder.Core
{
    [SupportedOSPlatform("windows")]
    internal class EventWriter
    {
        EventWatcher eventWatcher;
        IServiceProvider serviceProvider;

        ILogger<Worker> logger;

        internal EventWriter(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
            eventWatcher = new EventWatcher(OnEventReceived, logger);

        }

        internal async Task Start()
        {
            await eventWatcher.Attach();
        }

        internal async Task Stop()
        {
            await eventWatcher.Dettach();
        }

        internal async Task Restart()
        {
            await eventWatcher.Reattach();
        }

        static readonly XmlSerializer serializer = new XmlSerializer(typeof(Events.Raw.EventType));

        void OnEventReceived(object? sender, EventRecordWrittenEventArgs e) => Task.Run(() =>
        {
            if (e.EventRecord == null)
                return;

            string? XmlData = e.EventRecord.ToXml();

            if (XmlData == null)
            {
                return;
            }

            StringReader stringReader = new StringReader(XmlData);


            Events.Raw.EventType? eventType = serializer.Deserialize(stringReader) as Events.Raw.EventType;

            if (eventType == null)
                return;

            Events.EventType eventType1 = new Events.EventType(eventType, XmlData);

            using (var scope = serviceProvider.CreateScope())
            {
                using (var db = scope.ServiceProvider.GetRequiredService<EventDbContext>())
                {
                    db.Events.Add(eventType1);

                    db.SaveChanges();
                }
            }

            logger.LogInformation($"Event from log {e.EventRecord.LogName} recorded.");
        });
    }
}
