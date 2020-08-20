using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;

namespace DigitalLearningIntegration.Infraestructure.Repository.BussUnit
{
    public interface IBussUnitRepository : IRepository<UnidadesNegocio>
    {
        ResultDto CreatedOrUpdate(UnidadesNegocio entity);
        UnidadesNegocio GetByName(string name, int idSociedad);
    }
}
