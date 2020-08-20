using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Infraestructure.Repository.OcupLevel
{
    public interface IOcupLevelRepository : IRepository<NivelOcupacional>
    {
        ResultDto CreatedOrUpdate(NivelOcupacional entity);
        NivelOcupacional GetByName(string name, int idSociedad);
    }
}
