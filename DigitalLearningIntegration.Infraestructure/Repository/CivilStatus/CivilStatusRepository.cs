using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DigitalLearningIntegration.Infraestructure.Dto;

namespace DigitalLearningIntegration.Infraestructure.Repository.CivilStatus
{
    public class CivilStatusRepository : Repository<EstadoCivil>, ICivilStatusRepository
    {
        private readonly HCMKomatsuProdContext _context;

        public override EstadoCivil GetById(int id)
        {
            return _context.EstadoCivil.FirstOrDefault(x => x.Id == id);
        }

        public ResultDto CreatedOrUpdate(EstadoCivil entity)
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

        public EstadoCivil GetByName(string name)
        {
            var cleanName = Utils.Utils.CleanString(name).ToUpper();

            return _context.EstadoCivil.AsEnumerable().FirstOrDefault(g => Utils.Utils.CleanString(g.Nombre).ToUpper().Contains(cleanName));
        }

        public CivilStatusRepository(HCMKomatsuProdContext dataContext) : base(dataContext)
        {
            _context = dataContext;
        }
    }
}
