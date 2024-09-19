using System.Runtime.Versioning;

namespace InfinityOfficialNetwork.Management.OperatingSystem.Windows.EventRecorder.Core
{
    [SupportedOSPlatform("windows")]
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private readonly IServiceProvider serviceProvider;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
            eventWriter = new EventWriter(logger, this.serviceProvider);
        }

        EventWriter eventWriter;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {


            await eventWriter.Start();

            DateTime sinceLastRestart = DateTime.UtcNow;
            while (!stoppingToken.IsCancellationRequested)
            {
                if (DateTime.UtcNow - sinceLastRestart > new TimeSpan(0, 0, 10, 0, 0, 0))
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
