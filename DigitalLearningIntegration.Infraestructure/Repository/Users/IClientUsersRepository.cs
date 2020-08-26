using DigitalLearningDataImporter.DALstd;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System.Collections.Generic;

namespace DigitalLearningIntegration.Infraestructure.Repository.Users
{
    public interface IClientUsersRepository : IRepository<ClienteUsers>
    {
        ResultDto CreatedOrUpdate(ClienteUsers entity);
        IEnumerable<ClienteUsers> GetUsersByClientId(int clientId);
        ClienteUsers GetClientUsersByClientUserId(int? idClientes, int? idUser);
    }
}
