using Microsoft.EntityFrameworkCore;
using Telecomm360.Models;

namespace Telecomm360.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<Incident> Incidents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔑 Explicitly define the primary keys for all entities
            modelBuilder.Entity<User>().HasKey(u => u.UserID);
            modelBuilder.Entity<AuditLog>().HasKey(a => a.AuditID);
            modelBuilder.Entity<Notification>().HasKey(n => n.NotificationID);
            modelBuilder.Entity<RoleEntity>().HasKey(r => r.RoleID);
            modelBuilder.Entity<Alarm>().HasKey(a => a.AlarmID);
            modelBuilder.Entity<Incident>().HasKey(i => i.IncidentID);

            // 🔄 1. Alarm Enums -> Saved as Strings in Database
            modelBuilder.Entity<Alarm>()
                .Property(a => a.Severity)
                .HasConversion<string>();

            modelBuilder.Entity<Alarm>()
                .Property(a => a.Status)
                .HasConversion<string>();

            // 🔄 2. Incident Priority Enum -> Saved as String in Database
            // (Note: Change 'Priority' if your model uses a name like 'IncidentPriority')
            modelBuilder.Entity<Incident>()
                .Property(i => i.Priority) 
                .HasConversion<string>();

            // 🔄 3. Notification Status Enum -> Saved as String in Database
            // (Note: Change 'Status' if your model uses a name like 'NotificationStatus')
            modelBuilder.Entity<Notification>()
                .Property(n => n.Status)
                .HasConversion<string>();

            // 🔄 4. Role Status Enum -> Saved as String in Database
            // (Note: Change 'Status' if your model uses a name like 'RoleStatus')
            modelBuilder.Entity<RoleEntity>()
                .Property(r => r.Status)
                .HasConversion<string>();
        }
    }
}