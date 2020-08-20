using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using DigitalLearningIntegration.Infraestructure.Utils;
using System;
using System.Linq;

namespace DigitalLearningIntegration.Infraestructure.Repository.Genre
{
    public class GenreRepository : Repository<Genero>, IGenreRepository
    {
        private readonly HCMKomatsuProdContext _context;
        public GenreRepository(HCMKomatsuProdContext dataContext) : base(dataContext)
        {
            _context = dataContext;
        }
        public ResultDto CreatedOrUpdate(Genero entity)
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

        public override Genero GetById(int id)
        {
            return _context.Genero.FirstOrDefault(x => x.Id == id);
        }

        public Genero GetByName(string name)
        {
            return _context.Genero.AsEnumerable().FirstOrDefault(g => Utils.Utils.CleanString(g.Nombre).ToUpper() == Utils.Utils.CleanString(name).ToUpper());
        }
    }
}
