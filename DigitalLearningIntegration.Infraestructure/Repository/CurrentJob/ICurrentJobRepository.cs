using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Infraestructure.Repository.CurrentJob
{
    public interface ICurrentJobRepository : IRepository<PosicionLaboral>
    {
        ResultDto CreatedOrUpdate(PosicionLaboral entity);
        PosicionLaboral GetCurrentJobByPeopleSociety(int peopleId, int societyId);
    }
}
