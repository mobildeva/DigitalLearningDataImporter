using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Linq;

namespace DigitalLearningIntegration.Infraestructure.Repository.Local
{
    public class LocalRepository : Repository<Locales>, ILocalRepository
    {
        private readonly HCMKomatsuProdContext _context;
        public LocalRepository(HCMKomatsuProdContext dataContext) : base(dataContext)
        {
            _context = dataContext;
        }
        public ResultDto CreatedOrUpdate(Locales entity)
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

        public Locales GetByCode(string code)
        {
            var cleanCode = Utils.Utils.CleanString(code).ToUpper();

            return _context.Locales.AsEnumerable().FirstOrDefault(un => Utils.Utils.CleanString(un.CodigoLocal).ToUpper() == cleanCode);
        }

        public override Locales GetById(int id)
        {
            return _context.Locales.FirstOrDefault(x => x.Id == id);
        }

        public Locales GetByName(string name)
        {
            var cleanName = Utils.Utils.CleanString(name).ToUpper();

            return _context.Locales.AsEnumerable().FirstOrDefault(un => Utils.Utils.CleanString(un.NombreLocal).ToUpper() == cleanName);
        }
    }
}
