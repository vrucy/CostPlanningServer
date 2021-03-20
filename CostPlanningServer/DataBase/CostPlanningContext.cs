using CostPlanningServer.Model;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Configuration;

namespace CostPlanningServer.DataBase
{
    public class CostPlanningContext: DbContext
    {
        private readonly ILogger _logger;
        public CostPlanningContext(DbContextOptions options) : base(options) {  }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Device> Devices{ get; set; }
        public DbSet<SyncData<Order>> SyncDataOrder{ get; set; }
        public DbSet<SyncData<Category>> SyncDataCategory { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Hrana"
                }, new Category
                {
                    Id = 2,
                    Name = "Razno"
                }, new Category
                {
                    Id = 3,
                    Name = "Putovanja"
                }
            );

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Category)
                .WithMany(c => c.Orders)
                .HasForeignKey(k => k.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>().Ignore(s => s.SyncUser);
            modelBuilder.Entity<Category>().Ignore(s => s.SyncUser);
                        
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            //.AddJsonFile("appsettings.json")
            //.Build();
            base.OnConfiguring(optionsBuilder);
#if DEBUG
            const string SERVER = "CostPlanningDbTest";
#else

        const string SERVER = "CostPlanningDb";

#endif
            //var x = configuration.GetConnectionString(SERVER);
            //optionsBuilder.UseSqlServer(configuration.GetConnectionString(SERVER));

        }
    }
}
