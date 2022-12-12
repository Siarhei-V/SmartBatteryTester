using Microsoft.EntityFrameworkCore;
using SmartBatteryTesterWebApp.DAL.Entities;

namespace SmartBatteryTesterWebApp.DAL.EF
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Measurement>? Measurements { get; set; }
        public DbSet<MeasurementSet>? MeasurementSets { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    }
}
