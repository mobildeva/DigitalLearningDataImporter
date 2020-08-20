using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Infraestructure.Repository.Country
{
    public interface ICountryRepository : IRepository<Pais>
    {
        ResultDto CreatedOrUpdate(Pais entity);
        Pais GetByName(string name);
    }
}
