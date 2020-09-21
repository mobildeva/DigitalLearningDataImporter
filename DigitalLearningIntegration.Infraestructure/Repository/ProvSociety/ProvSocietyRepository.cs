using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalLearningIntegration.Infraestructure.Repository.ProvSociety
{
    public class ProvSocietyRepository : Repository<SociedadProveedor>, IProvSocietyRepository
    {
        private readonly HCMKomatsuProdContext _dataContext;
        public ProvSocietyRepository(HCMKomatsuProdContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }
        public ResultDto CreatedOrUpdate(SociedadProveedor entity)
        {
            ResultDto Result = new ResultDto
            {
                Result = true,
                Message = "Success"
            };
            try
            {
                if (entity.Id.Equals(0))
                {
                    Add(entity);
                    Result.Id = entity.Id;
                }
                else
                {
                    Update(entity);
                }
            }
            catch (Exception ex)
            {
                Result = new ResultDto
                {
                    Result = false,
                    Message = ex.Message
                };
            }
            return Result;
        }

        public override SociedadProveedor GetById(int id)
        {
            return _dataContext.SociedadProveedor.FirstOrDefault(x => x.Id == id);
        }

        public SociedadProveedor GetBySocietyProv(int societyId, int provId, int? societyTypeId)
        {
            return _dataContext.SociedadProveedor.FirstOrDefault(x => x.IdSociedad == societyId && x.IdProveedor == provId && (!societyTypeId.HasValue || x.IdTipoSociedad == societyTypeId));
        }

    }
}
