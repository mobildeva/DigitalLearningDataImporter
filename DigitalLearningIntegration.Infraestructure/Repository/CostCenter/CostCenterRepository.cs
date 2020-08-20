using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DigitalLearningIntegration.Infraestructure.Repository.CostCenter
{
    public class CostCenterRepository : Repository<CentroCosto>, ICostCenterRepository
    {
        private readonly HCMKomatsuProdContext _context;
        public CostCenterRepository(HCMKomatsuProdContext dataContext) : base(dataContext)
        {
            _context = dataContext;
        }
        public ResultDto CreatedOrUpdate(CentroCosto entity)
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

        public override CentroCosto GetById(int id)
        {
            return _context.CentroCosto.FirstOrDefault(x => x.Id == id);
        }

        public CentroCosto GetByName(string name, int societyId)
        {
            return _context.CentroCosto.AsEnumerable().FirstOrDefault(s => (s.IdSociedad == societyId) && (Utils.Utils.CleanString(s.Nombre).ToUpper() == Utils.Utils.CleanString(name).ToUpper()));
        }
    }
}
