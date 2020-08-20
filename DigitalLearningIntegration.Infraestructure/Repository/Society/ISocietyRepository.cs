using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Infraestructure.Repository.Society
{
    public interface ISocietyRepository : IRepository<Sociedad>
    {
        ResultDto CreatedOrUpdate(Sociedad entity);
        Sociedad GetByUniqueIdentifier(string uniqIdent);
        Sociedad GetByName(string name);
    }
}
