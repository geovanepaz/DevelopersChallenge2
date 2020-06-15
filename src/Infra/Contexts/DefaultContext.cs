using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Domain;
using System.IO;

namespace Infra.Contexts
{
    public class DefaultContext : DbContext
    {
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options) { }

        public DefaultContext()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public DbSet<Transaction> Transactions { get; set; }

        private readonly IConfiguration _configuration;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DefaultContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
