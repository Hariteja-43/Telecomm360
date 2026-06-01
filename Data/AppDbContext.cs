using Microsoft.EntityFrameworkCore;
using Telecom360.Models;

namespace Telecom360.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // ✅ DbSets (Tables)
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ComplianceReport> ComplianceReports { get; set; }
        public DbSet<RetentionPolicy> RetentionPolicies { get; set; }

        // ✅ Model Configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
                entity.HasOne(o => o.product)          // Navigation property in Order
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