using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Infraestructure.Repository.OrgUnit
{
    public interface IOrgUnitRepository : IRepository<UnidadesOrganizacional>
    {
        ResultDto CreatedOrUpdate(UnidadesOrganizacional entity);
        UnidadesOrganizacional GetByName(string name, int idSociedad);
    }
}
