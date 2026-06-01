using Microsoft.EntityFrameworkCore;
using Telecomm360.Models;

namespace Telecomm360.Data;

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
    
}

