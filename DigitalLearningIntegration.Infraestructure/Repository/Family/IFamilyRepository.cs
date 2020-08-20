using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;

namespace DigitalLearningIntegration.Infraestructure.Repository.Family
{
    public interface IFamilyRepository : IRepository<FamiliaCargo>
    {
        ResultDto CreatedOrUpdate(FamiliaCargo entity);
        FamiliaCargo GetByName(string name, int societyId);
    }
}
