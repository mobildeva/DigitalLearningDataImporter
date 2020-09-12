using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Linq;

namespace DigitalLearningIntegration.Infraestructure.Repository.BussUnit
{
    public class BussUnitRepository : Repository<UnidadesNegocio>, IBussUnitRepository
    {
        private readonly HCMKomatsuProdContext _context;
        public BussUnitRepository(HCMKomatsuProdContext context) : base(context)
        {
            _context = context;
        }

        public ResultDto CreatedOrUpdate(UnidadesNegocio entity)
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

        public override UnidadesNegocio GetById(int id)
        {
            return _context.UnidadesNegocio.FirstOrDefault(x => x.Id == id);
        }

        public UnidadesNegocio GetByName(string name, int idSociedad)
        {
            var cleanName = Utils.Utils.CleanString(name).ToUpper();

            return _context.UnidadesNegocio.AsEnumerable().FirstOrDefault(un => Utils.Utils.CleanString(un.Nombre).ToUpper() == cleanName && un.IdSociedad == idSociedad);
        }
    }
}
