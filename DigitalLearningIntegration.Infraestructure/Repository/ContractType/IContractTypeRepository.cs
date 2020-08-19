using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;

namespace DigitalLearningIntegration.Infraestructure.Repository.ContractType
{
    public interface IContractTypeRepository : IRepository<TipoContrato>
    {
        ResultDto CreatedOrUpdate(TipoContrato entity);
    }
}
