using System.Runtime.Versioning;

namespace InfinityOfficialNetwork.Management.OperatingSystem.Windows.EventRecorder.Core
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;

        public Worker(ILogger<Worker> logger)
        {
            this.logger = logger;
            eventWriter = new EventWriter(logger, "events.db");
        }

        EventWriter eventWriter;

        [SupportedOSPlatform("windows")]
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await eventWriter.Start();
            
            DateTime sinceLastRestart = DateTime.UtcNow;
            while (!stoppingToken.IsCancellationRequested)
            {
                if (DateTime.UtcNow - sinceLastRestart > new TimeSpan(0,0,10,0,0,0))
                {
                    await eventWriter.Restart();
                    sinceLastRestart = DateTime.UtcNow;
                }
                await Task.Delay(1000, stoppingToken);
            }
            await eventWriter.Stop();
        }
    }
}
