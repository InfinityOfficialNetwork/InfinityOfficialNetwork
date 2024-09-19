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
        public EventDbContext(DbContextOptions<EventDbContext> options)
            : base(options)
        {
        }




        public DbSet<Events.EventType> Events { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    try
        //    {
        //        optionsBuilder.UseSqlServer(@"Server=ION-PC-0017;Database=events;User ID=events_login;Password=;Trusted_Connection=True;TrustServerCertificate=True");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Failed to connect to the databse", ex);
        //    }
        //}
    }
}
