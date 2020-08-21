using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;

namespace DigitalLearningIntegration.Infraestructure.Repository.Local
{
    public interface ILocalRepository : IRepository<Locales>
    {
        ResultDto CreatedOrUpdate(Locales entity);
        Locales GetByCode(string code);
        Locales GetByName(string name);
    }
}
