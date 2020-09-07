using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Linq;

namespace DigitalLearningIntegration.Infraestructure.Repository.Family
{
    public class FamilyRepository : Repository<FamiliaCargo>, IFamilyRepository
    {
        private readonly HCMKomatsuProdContext _context;
        public FamilyRepository(HCMKomatsuProdContext dataContext) : base(dataContext)
        {
            _context = dataContext;
        }
        public ResultDto CreatedOrUpdate(FamiliaCargo entity)
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

        public override FamiliaCargo GetById(int id)
        {
            return _context.FamiliaCargo.FirstOrDefault(x => x.Id == id);
        }

        public FamiliaCargo GetByName(string name, int societyId)
        {
            var cleanName = Utils.Utils.CleanString(name).ToUpper();

            return _context.FamiliaCargo.AsEnumerable().FirstOrDefault(s => (!s.IdSociedad.HasValue || s.IdSociedad == societyId) && (Utils.Utils.CleanString(s.Nombre).ToUpper() == cleanName));
        }
    }
}
