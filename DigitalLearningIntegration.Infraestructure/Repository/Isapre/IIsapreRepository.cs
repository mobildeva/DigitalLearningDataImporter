using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Infraestructure.Repository.Isapre
{
    public interface IIsapreRepository : IRepository<Isapres>
    {
        ResultDto CreatedOrUpdate(Isapres entity);
        Isapres GetByName(string name);
    }
}
