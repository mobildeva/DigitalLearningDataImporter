using Microsoft.EntityFrameworkCore;
using System.Configuration;
using DigitalLearningIntegration.Infraestructure.Migrations.SeedData;
using DigitalLearningDataImporter.DALstd;

namespace DigitalLearningIntegration.Infraestructure.Migrations
{
    public class Context : DbContext
    {
        public DbSet<GopEntity> GopEntitys { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["Test"].ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GobEntityConfiguration());
        }
    }
}
