using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using DigitalLearningIntegration.Infraestructure.Dto;

namespace DigitalLearningIntegration.Infraestructure.Repository.CivilStatus
{
    public interface ICivilStatusRepository : IRepository<EstadoCivil>
    {
        ResultDto CreatedOrUpdate(EstadoCivil entity);
        EstadoCivil GetByName(string name);
    }
}
