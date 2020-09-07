using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalLearningIntegration.Infraestructure.Repository.Job
{
    public class JobRepository : Repository<Cargos>, IJobRepository
    {
        private readonly HCMKomatsuProdContext _context;
        public JobRepository(HCMKomatsuProdContext dataContext) : base(dataContext)
        {
            _context = dataContext;
        }
        public ResultDto CreatedOrUpdate(Cargos entity)
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

        public override Cargos GetById(int id)
        {
            return _context.Cargos.FirstOrDefault(x => x.Id == id);
        }

        public Cargos GetByName(string name)
        {
            var cleanName = Utils.Utils.CleanString(name).ToUpper();

            return _context.Cargos.AsEnumerable().FirstOrDefault(g => Utils.Utils.CleanString(g.Nombre).ToUpper() == cleanName);
        }
    }
}
