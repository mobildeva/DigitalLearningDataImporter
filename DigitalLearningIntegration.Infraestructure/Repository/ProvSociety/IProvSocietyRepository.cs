using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Infraestructure.Repository.ProvSociety
{
    public interface IProvSocietyRepository : IRepository<SociedadProveedor>
    {
        ResultDto CreatedOrUpdate(SociedadProveedor entity);        
        SociedadProveedor GetBySocietyProv(int societyId, int provId, int? societyTypeId);
    }
}
