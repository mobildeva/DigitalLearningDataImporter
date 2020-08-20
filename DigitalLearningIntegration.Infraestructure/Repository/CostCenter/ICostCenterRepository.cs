using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Infraestructure.Repository.CostCenter
{
    public interface ICostCenterRepository : IRepository<CentroCosto>
    {
        ResultDto CreatedOrUpdate(CentroCosto entity);
        CentroCosto GetByName(string name, int societyId);
    }
}
