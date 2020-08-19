using DigitalLearningDataImporter.DALstd;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalLearningIntegration.Infraestructure.Migrations.SeedData
{
    public class GobEntityConfiguration : IEntityTypeConfiguration<GopEntity>
    {
        //private readonly Context _context;
        //public RoleConfiguration(Context context)
        //{
        //    _context = context;
        //}
        public GobEntityConfiguration()
        {
        }
        public void Configure(EntityTypeBuilder<GopEntity> builder)
        {
            //if (_context.Roles.Count() == 0)
            //{

            builder.HasData(new GopEntity[]{
                    new GopEntity
                    {
                        FullName = "Aaron",
                        IsDeleted = false
                    }
                });
        }
    }
}