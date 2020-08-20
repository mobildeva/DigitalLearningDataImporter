using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;

namespace DigitalLearningIntegration.Infraestructure.Repository.BloodG
{
    public interface IBloodGRepository : IRepository<GrupoSanguineo>
    {
        ResultDto CreatedOrUpdate(GrupoSanguineo entity);
        GrupoSanguineo GetByName(string name);
    }
}
