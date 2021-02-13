using CostPlanningServer.Model;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CostPlanningServer.DataBase
{
    public class CostPlanningContext: DbContext
    {
        private readonly ILogger _logger;
        public CostPlanningContext(DbContextOptions options) : base(options) {  }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SyncUser<Order>> SyncUserOrder{ get; set; }
        public DbSet<SyncUser<Category>> SyncUserCategory { get; set; }
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
           


            //modelBuilder.Entity<UpdatedUserBaseVisibility>()
            //    .HasOne(uu => uu.UpdatedUser)
            //    .WithMany(uuv => uuv.UpdatedUserBaseVisibilities)
            //    .HasForeignKey(uu => uu.UpdatedUserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Category)
                .WithMany(c => c.Orders)
                .HasForeignKey(k => k.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); 
            
            //modelBuilder.Entity<Category>()
            //    .HasOne(o => o.SyncVisible)
            //    .WithMany(c => c.SyncUsers)
            //    .HasForeignKey(k => k.CategoryId)
            //    .OnDelete(DeleteBehavior.Restrict);


            
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
