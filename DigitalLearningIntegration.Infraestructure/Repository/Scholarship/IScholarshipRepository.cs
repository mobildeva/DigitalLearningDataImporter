using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Infraestructure.Repository.Scholarship
{
    public interface IScholarshipRepository : IRepository<EscolaridadSence>
    {
        ResultDto CreatedOrUpdate(EscolaridadSence entity);
        EscolaridadSence GetByName(string name);
    }
}
