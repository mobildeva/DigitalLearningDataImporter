using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Infraestructure.Repository.Society
{
    public interface ISocietyTypeRepository : IRepository<TipoSociedad>
    {
        ResultDto CreatedOrUpdate(TipoSociedad entity);
        TipoSociedad GetByName(string name);
    }
}
