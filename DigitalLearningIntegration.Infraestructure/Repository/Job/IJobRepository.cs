using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Infraestructure.Repository.Job
{
    public interface IJobRepository : IRepository<Cargos>
    {
        ResultDto CreatedOrUpdate(Cargos entity);
        Cargos GetByName(string name);
    }
}
