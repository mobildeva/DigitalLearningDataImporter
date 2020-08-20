using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Infraestructure.Repository.WorkingDay
{
    public interface IWorkingDayRepository : IRepository<JornadaLaboral>
    {
        ResultDto CreatedOrUpdate(JornadaLaboral entity);
        JornadaLaboral GetByName(string name);
    }
}
