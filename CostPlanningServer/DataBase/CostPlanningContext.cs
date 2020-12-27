using CostPlanningServer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
            
            //modelBuilder.Entity<User>().HasData(
            //    new User
            //    {
            //        Id = 1,
            //        FirstName = "Vladimir",
            //        LastName = "Vrucinic"
            //    }, new User
            //    {
            //        Id = 2,
            //        FirstName = "Jovana",
            //        LastName = "Vrucinic"
            //    }
            //    );
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
