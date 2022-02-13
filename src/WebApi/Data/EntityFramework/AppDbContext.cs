using Microsoft.EntityFrameworkCore;
using WebApi.Data.Domains;
using WebApi.Data.EntityFramework.Configs;

namespace WebApi.Data.EntityFramework
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) { }
        
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }

    // public class AppDbContextFactory
    // {
    //     
    // }
}