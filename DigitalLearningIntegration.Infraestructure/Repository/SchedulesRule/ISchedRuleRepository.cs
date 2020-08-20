using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Infraestructure.Repository.SchedulesRule
{
    public interface ISchedRuleRepository : IRepository<ReglaPlanHorario>
    {
        ResultDto CreatedOrUpdate(ReglaPlanHorario entity);
        ReglaPlanHorario GetByName(string name);
    }
}
