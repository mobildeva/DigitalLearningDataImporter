using DigitalLearningDataImporter.DALstd.ProdEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;

namespace DigitalLearningIntegration.Infraestructure.Repository.WorkingDay
{
    public class WorkingDayRepository : Repository<JornadaLaboral>, IWorkingDayRepository
    {
        private readonly HCMKomatsuProdContext _context;
        public WorkingDayRepository(HCMKomatsuProdContext context) : base(context)
        {
            _context = context;
        }

        public ResultDto CreatedOrUpdate(JornadaLaboral entity)
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

        public override JornadaLaboral GetById(int id)
        {
            return _context.JornadaLaboral.FirstOrDefault(x => x.Id == id);
        }

        public JornadaLaboral GetByName(string name)
        {
            var cleanName = Utils.Utils.CleanString(name).ToUpper();

            return _context.JornadaLaboral.AsEnumerable().FirstOrDefault(un => Utils.Utils.CleanString(un.Nombre).ToUpper() == cleanName);
        }
    }
}
