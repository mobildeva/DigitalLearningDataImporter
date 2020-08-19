using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Linq;

namespace DigitalLearningIntegration.Infraestructure.Repository.Afp
{
    public class AfpRepository : Repository<DigitalLearningDataImporter.DALstd.ProdEntities.Afp>, IAfpRepository
    {
        private readonly HCMKomatsuProdContext _context;
        public AfpRepository(HCMKomatsuProdContext context) : base(context)
        {
            _context = context;
        }

        public ResultDto CreatedOrUpdate(DigitalLearningDataImporter.DALstd.ProdEntities.Afp entity)
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

        public override DigitalLearningDataImporter.DALstd.ProdEntities.Afp GetById(int id)
        {
            return _context.Afp.FirstOrDefault(x => x.Id == id);
        }
    }
}
