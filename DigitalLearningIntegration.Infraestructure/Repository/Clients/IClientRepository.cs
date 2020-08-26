using DigitalLearningDataImporter.DALstd;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Infraestructure.Repository.Clients
{
    public interface IClientRepository : IRepository<Clientes>
    {
        Clientes GetClientBySocietyId(int societyId);
    }
}
