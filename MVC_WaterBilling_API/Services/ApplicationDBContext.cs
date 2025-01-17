using Microsoft.EntityFrameworkCore;
using MVC_WaterBilling_API.Model.Advance;
using MVC_WaterBilling_API.Model.Bill;
using MVC_WaterBilling_API.Model.Consumer;
using MVC_WaterBilling_API.Model.Meter_Reading;
using MVC_WaterBilling_API.Model.Payments;
using MVC_WaterBilling_API.Model.Settings;
using MVC_WaterBilling_API.Model.User;
using System.Diagnostics.Metrics;

namespace MVC_WaterBilling_API.Services
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options) { }
        public DbSet<Users> Users { get; set; }
        public DbSet<Consumers> Consumers { get; set; }
        public DbSet<MeterReading> Meters { get; set; }
        public DbSet<Bills> Bills { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<Advances> Advances { get; set; }
        public DbSet<Settings> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasKey(u => u.UserID);
            modelBuilder.Entity<Consumers>().HasKey(c => c.ConsumerID);
            modelBuilder.Entity<MeterReading>().HasKey(m => m.ReadingID);
            modelBuilder.Entity<Bills>().HasKey(b => b.BillID);
            modelBuilder.Entity<Payments>().HasKey(p => p.PaymentID);
            modelBuilder.Entity<Advances>().HasKey(a => a.AdvanceID);
            modelBuilder.Entity<Settings>().HasKey(s => s.SettingID);

            base.OnModelCreating(modelBuilder);
        }



    }
}
