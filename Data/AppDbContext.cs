using Microsoft.EntityFrameworkCore;
using Telecom360.Models;
using Telecomm360.Model;
using Telecomm360.Models;

namespace Telecomm360.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            
        }
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

             base.OnModelCreating(modelBuilder);

            // ✅ Product Configuration
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.ProductId);

                entity.Property(p => p.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(p => p.Category)
                      .HasMaxLength(50);

                entity.Property(p => p.PriceModel)
                      .HasMaxLength(50);

                entity.Property(p => p.Status)
                      .HasConversion<string>()
                      .IsRequired();
            });

            // ✅ Order Configuration (UPDATED WITH FOREIGN KEY)
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.OrderID);

                entity.HasIndex(o => o.ProductID);
                // ✅ ✅ FOREIGN KEY RELATIONSHIP (IMPORTANT)
                entity.HasOne(o => o.Product)          // Navigation property in Order
                      .WithMany()                     // One Product → Many Orders
                      .HasForeignKey(o => o.ProductID)
                      .OnDelete(DeleteBehavior.Restrict); 
                      // Use Cascade if you want deleting product to delete orders
                  
                  entity.HasIndex(o => o.SubscriberID);

                entity.Property(o => o.Status)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(o => o.FulfillmentSteps)
                      .HasMaxLength(200);

                entity.Property(o => o.OrderDate)
                      .IsRequired();
                
            });

            // ✅ Compliance Report Configuration
            modelBuilder.Entity<ComplianceReport>(entity =>
            {
                entity.HasKey(c => c.ReportId);

                entity.Property(c => c.Type)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(c => c.Scope)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(c => c.GeneratedDate)
                      .IsRequired();
            });

            // ✅ Retention Policy Configuration
            modelBuilder.Entity<RetentionPolicy>(entity =>
            {
                entity.HasKey(r => r.PolicyID);

                entity.Property(r => r.DataType)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(r => r.RetentionPeriod)
                      .IsRequired();

                entity.Property(r => r.AppliedFrom)
                      .IsRequired();
            });
        }
    }
}
