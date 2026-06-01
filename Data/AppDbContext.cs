using Microsoft.EntityFrameworkCore;
using Telecomm360.Model;

namespace Telecomm360.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            
        }

        //  DbSets should be OUTSIDE constructor
        //AppDbContext constructor should only contain the base call to DbContext, and the DbSet properties should be defined outside of the constructor. This allows Entity Framework to properly recognize and configure the entities when setting up the database context.

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Subscriber> Subscribers { get; set; }

        public DbSet<NetworkResource> NetworkResources { get; set; }

        public DbSet<ProvisioningTask> ProvisioningTasks { get; set; }
    }
}
