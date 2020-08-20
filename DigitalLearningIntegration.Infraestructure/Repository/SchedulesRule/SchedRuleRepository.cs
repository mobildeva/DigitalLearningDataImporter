using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalLearningIntegration.Infraestructure.Repository.SchedulesRule
{
    public class SchedRuleRepository : Repository<ReglaPlanHorario>, ISchedRuleRepository
    {
        private readonly HCMKomatsuProdContext _context;
        public SchedRuleRepository(HCMKomatsuProdContext context) : base(context)
        {
            _context = context;
        }

        public ResultDto CreatedOrUpdate(ReglaPlanHorario entity)
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

        public override ReglaPlanHorario GetById(int id)
        {
            return _context.ReglaPlanHorario.FirstOrDefault(x => x.Id == id);
        }

        public ReglaPlanHorario GetByName(string name)
        {
            return _context.ReglaPlanHorario.AsEnumerable().FirstOrDefault(un => Utils.Utils.CleanString(un.Nombre).ToUpper() == Utils.Utils.CleanString(name).ToUpper());
        }
    }
}
