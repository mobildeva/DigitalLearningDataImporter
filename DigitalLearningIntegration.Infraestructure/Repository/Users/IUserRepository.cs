using System.Collections.Generic;
using DigitalLearningDataImporter.DALstd;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;

namespace DigitalLearningIntegration.Infraestructure.Repository.Users
{
    public interface IUserRepository : IRepository<DigitalLearningDataImporter.DALstd.Users>
    {
        ResultDto CreatedOrUpdate(DigitalLearningDataImporter.DALstd.Users entity);
        DigitalLearningDataImporter.DALstd.Users GetUserByRUTUserName(string usernameRut);
        //void AddRangeOfUsers(IEnumerable<DigitalLearningDataImporter.DALstd.Users> newUsers);
        DigitalLearningDataImporter.DALstd.Users GetByUserName(string userName);
    }
}
