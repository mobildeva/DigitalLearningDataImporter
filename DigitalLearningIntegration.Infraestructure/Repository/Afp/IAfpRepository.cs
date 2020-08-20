using System;
using System.Collections.Generic;
using System.Text;
using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;

namespace DigitalLearningIntegration.Infraestructure.Repository.Afp
{
    public interface IAfpRepository : IRepository<DigitalLearningDataImporter.DALstd.ProdEntities.Afp>
    {
        ResultDto CreatedOrUpdate(DigitalLearningDataImporter.DALstd.ProdEntities.Afp entity);
        DigitalLearningDataImporter.DALstd.ProdEntities.Afp GetAfpByName(string name);
    }
}
