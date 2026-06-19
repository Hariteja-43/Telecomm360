using Microsoft.EntityFrameworkCore;
using Telecom360.Model;
using Telecomm360.Model;

namespace Telecomm360.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<UsageRecord> UsageRecords { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<KPIReport> KPIReports { get; set; }
        public DbSet<AnalyticsDataset> AnalyticsDatasets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ComplianceReport> ComplianceReports { get; set; }
        public DbSet<RetentionPolicy> RetentionPolicies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<NetworkResource> NetworkResources { get; set; }
        public DbSet<ProvisioningTask> ProvisioningTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================
            // PRIMARY KEYS
            // =========================
            modelBuilder.Entity<User>().HasKey(u => u.UserID);
            modelBuilder.Entity<AuditLog>().HasKey(a => a.AuditLogID);
            modelBuilder.Entity<Notification>().HasKey(n => n.NotificationID);
            modelBuilder.Entity<RoleEntity>().HasKey(r => r.RoleID);
            modelBuilder.Entity<Alarm>().HasKey(a => a.AlarmID);
            modelBuilder.Entity<Incident>().HasKey(i => i.IncidentID);
            modelBuilder.Entity<Product>().HasKey(p => p.ProductId);
            modelBuilder.Entity<Order>().HasKey(o => o.OrderID);
            modelBuilder.Entity<ComplianceReport>().HasKey(c => c.ComplianceReportId);
            modelBuilder.Entity<RetentionPolicy>().HasKey(r => r.RetentionPeriodId);

            // =========================
            // FOREIGN KEYS
            // =========================

            // 🔹 User → Role
            modelBuilder.Entity<User>()
                .HasOne(u => u.RoleEntity)
                .WithMany()
                .HasForeignKey(u => u.RoleID)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔹 AuditLog → User FIXED
            modelBuilder.Entity<AuditLog>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔹 Notification → Subscriber
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Subscriber)
                .WithMany()
                .HasForeignKey(n => n.SubscriberID)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔹 Incident → Alarm
            modelBuilder.Entity<Incident>()
                .HasOne(i => i.Alarm)
                .WithMany()
                .HasForeignKey(i => i.AlarmID)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔹 Order → Product
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Product)
                .WithMany()
                .HasForeignKey(o => o.ProductID)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔹 Subscriber → Customer FIXED naming
            modelBuilder.Entity<Subscriber>()
                .HasOne(s => s.Customer)
                .WithMany()
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔹 ProvisioningTask → Order FIXED FK NAME
            modelBuilder.Entity<ProvisioningTask>()
                .HasOne(p => p.Order)
                .WithMany()
                .HasForeignKey(p => p.OrderId) // must match property EXACTLY
                .OnDelete(DeleteBehavior.Cascade);

            // 🔹 ProvisioningTask → Subscriber
            modelBuilder.Entity<ProvisioningTask>()
                .HasOne(p => p.Subscriber)
                .WithMany()
                .HasForeignKey(p => p.SubscriberId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔹 Order → Subscriber
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Subscriber)
                .WithMany()
                .HasForeignKey(o => o.SubscriberID)
                .OnDelete(DeleteBehavior.Restrict);

            // =========================
            // ENUM → STRING
            // =========================
            modelBuilder.Entity<Alarm>()
                .Property(a => a.Severity)
                .HasConversion<string>();

            modelBuilder.Entity<Alarm>()
                .Property(a => a.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Incident>()
                .Property(i => i.Priority)
                .HasConversion<string>();

            modelBuilder.Entity<Notification>()
                .Property(n => n.Status)
                .HasConversion<string>();

            modelBuilder.Entity<RoleEntity>()
                .Property(r => r.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Product>()
                .Property(p => p.Status)
                .HasConversion<string>();
        }
    }
}
