using System.Collections.Generic;
using DigitalLearningDataImporter.DALstd;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;

namespace DigitalLearningIntegration.Infraestructure.Repository.Users
{
    public interface IUserRepository : IRepository<DigitalLearningDataImporter.DALstd.Users>
    {
        ResultDto CreatedOrUpdate(DigitalLearningDataImporter.DALstd.Users entity);
    }
}
