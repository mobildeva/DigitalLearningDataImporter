using System;
using System.Collections.Generic;
using System.Text;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;

namespace DigitalLearningIntegration.Infraestructure.Repository.Peoples
{
    public interface IPeoplesRepository : IRepository<Personas>
    {
        ResultDto CreatedOrUpdate(Personas entity);
        Personas GetById(int id);
    }
}
