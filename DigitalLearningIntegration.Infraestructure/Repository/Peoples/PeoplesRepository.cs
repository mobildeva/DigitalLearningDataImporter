using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Linq;

namespace DigitalLearningIntegration.Infraestructure.Repository.Peoples
{
    public class PeoplesRepository : Repository<Personas>, IPeoplesRepository
    {
        private readonly HCMKomatsuProdContext _dataContext;

        public PeoplesRepository(HCMKomatsuProdContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }
        public ResultDto CreatedOrUpdate(Personas entity)
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

        public override Personas GetById(int id)
        {
            return _dataContext.Personas.FirstOrDefault(x => x.Id == id);
        }

        public Personas GetByIuAndDv(string uniqueId, string dv)
        {
            var uniqTrim = uniqueId.Trim();
            var dvtrim = dv.Trim();
            return _dataContext.Personas.FirstOrDefault(p => p.IdentificacionUnica == uniqTrim && p.Dv == dvtrim);
        }

        public Personas GetByIdConexion(int id)
        {
            return _dataContext.Personas.FirstOrDefault(x => x.IdConexion == id);
        }
    }
}
