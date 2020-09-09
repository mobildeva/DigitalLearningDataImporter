using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DigitalLearningIntegration.Infraestructure.Repository.Isapre
{
    public class IsapreRepository : Repository<Isapres>, IIsapreRepository
    {
        private readonly HCMKomatsuProdContext _context;
        public IsapreRepository(HCMKomatsuProdContext context) : base(context)
        {
            _context = context;
        }

        public ResultDto CreatedOrUpdate(Isapres entity)
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

        public override Isapres GetById(int id)
        {
            return _context.Isapres.FirstOrDefault(x => x.Id == id);
        }

        public Isapres GetByName(string name)
        {
            var cleanName = Utils.Utils.CleanString(name).ToUpper();

            return _context.Isapres.AsEnumerable().FirstOrDefault(un => Utils.Utils.CleanString(un.Nombre).ToUpper() == cleanName);
        }
    }
}
