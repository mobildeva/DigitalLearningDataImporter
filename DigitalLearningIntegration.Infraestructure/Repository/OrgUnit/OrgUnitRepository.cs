using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalLearningIntegration.Infraestructure.Repository.OrgUnit
{
    public class OrgUnitRepository : Repository<UnidadesOrganizacional>, IOrgUnitRepository
    {
        private readonly HCMKomatsuProdContext _context;
        public OrgUnitRepository(HCMKomatsuProdContext context) : base(context)
        {
            _context = context;
        }

        public ResultDto CreatedOrUpdate(UnidadesOrganizacional entity)
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

        public override UnidadesOrganizacional GetById(int id)
        {
            return _context.UnidadesOrganizacional.FirstOrDefault(x => x.Id == id);
        }

        public UnidadesOrganizacional GetByIdSociedad(int societyId)
        {
            return _context.UnidadesOrganizacional.FirstOrDefault(x => x.IdSociedad == societyId && x.Nombre != "Sin Información");
        }

        public UnidadesOrganizacional GetByName(string name, int idSociedad)
        {
            var cleanName = Utils.Utils.CleanString(name).ToUpper();
            return _context.UnidadesOrganizacional.AsEnumerable().FirstOrDefault(un => Utils.Utils.CleanString(un.Nombre).ToUpper() == cleanName && un.IdSociedad == idSociedad);
        }
    }
}
