using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Linq;

namespace DigitalLearningIntegration.Infraestructure.Repository.Country
{
    public class CountryRepository : Repository<Pais>, ICountryRepository
    {
        private readonly HCMKomatsuProdContext _context;
        public CountryRepository(HCMKomatsuProdContext dataContext) : base(dataContext)
        {
            _context = dataContext;
        }
        public ResultDto CreatedOrUpdate(Pais entity)
        {
            ResultDto Result = new ResultDto
            {
                Result = true,
                Message = "Success"
            };
            try
            {
                if (entity.IdPais.Equals(0))
                {
                    Add(entity);
                    Result.Id = entity.IdPais;
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

        public override Pais GetById(int id)
        {
            return _context.Pais.FirstOrDefault(x => x.IdPais == id);
        }

        public Pais GetByName(string name)
        {
            return _context.Pais.AsEnumerable().FirstOrDefault(g => (Utils.Utils.CleanString(g.Nombre).ToUpper() == Utils.Utils.CleanString(name).ToUpper()) || (Utils.Utils.CleanString(g.Nombre).ToUpper().Contains(Utils.Utils.CleanString(name).ToUpper())));
        }
    }
}
