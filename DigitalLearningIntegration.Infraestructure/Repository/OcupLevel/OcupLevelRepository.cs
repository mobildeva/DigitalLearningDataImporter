using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DigitalLearningIntegration.Infraestructure.Dto;

namespace DigitalLearningIntegration.Infraestructure.Repository.OcupLevel
{
    public class OcupLevelRepository : Repository<NivelOcupacional>, IOcupLevelRepository
    {
        private readonly HCMKomatsuProdContext _context;
        public OcupLevelRepository(HCMKomatsuProdContext context) : base(context)
        {
            _context = context;
        }

        public ResultDto CreatedOrUpdate(NivelOcupacional entity)
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

        public override NivelOcupacional GetById(int id)
        {
            return _context.NivelOcupacional.FirstOrDefault(x => x.Id == id);
        }

        public NivelOcupacional GetByName(string name, int idSociedad)
        {
            return _context.NivelOcupacional.AsEnumerable().FirstOrDefault(un => Utils.Utils.CleanString(un.Nombre).ToUpper() == Utils.Utils.CleanString(name).ToUpper() && un.IdSociedad == idSociedad);
        }
    }
}
