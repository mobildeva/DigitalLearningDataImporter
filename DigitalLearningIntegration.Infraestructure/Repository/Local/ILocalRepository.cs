using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;

namespace DigitalLearningIntegration.Infraestructure.Repository.Local
{
    public interface ILocalRepository
    {
        ResultDto CreatedOrUpdate(Locales entity);
        Locales GetByCode(string code);
        Locales GetByName(string name);
    }
}
