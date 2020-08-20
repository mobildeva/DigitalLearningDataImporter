using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System.Collections.Generic;

namespace DigitalLearningIntegration.Infraestructure.Repository.PersonalInfo
{
    public interface IPersonalInfoRepository : IRepository<InformacionPersonal>
    {
        ResultDto CreatedOrUpdate(InformacionPersonal entity);
        InformacionPersonal GetByPeopleId(int peopleId);
    }
}
