using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;

namespace DigitalLearningIntegration.Infraestructure.Repository.Genre
{
    public interface IGenreRepository : IRepository<Genero>
    {
        ResultDto CreatedOrUpdate(Genero entity);
        Genero GetByName(string name);
    }
}
