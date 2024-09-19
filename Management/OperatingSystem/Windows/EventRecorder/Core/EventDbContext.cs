using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfinityOfficialNetwork.Management.OperatingSystem.Windows.EventRecorder.Core
{
    public class EventDbContext : DbContext
    {
        public EventDbContext(ILogger<Worker> logger, string SQliteDbFileName)
        {
            this.logger = logger;
            sqliteDbFileName = SQliteDbFileName;
        }

        readonly string sqliteDbFileName;

        ILogger<Worker> logger;

        public DbSet<Events.EventType> Events { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                logger.LogInformation("Database attaching to SQLite file");
                optionsBuilder.UseSqlite($"Data Source={sqliteDbFileName}");
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "An error occured while connecting to the database, see inner exception for details.");
                throw new Exception("Failed to connect to the databse", ex);
            }
        }
    }
}
