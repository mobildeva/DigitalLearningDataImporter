using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using DigitalLearningIntegration.Infraestructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalLearningIntegration.Infraestructure.Repository.Society
{
    public class SocietyTypeRepository : Repository<TipoSociedad>, ISocietyTypeRepository
    {
        private readonly HCMKomatsuProdContext _dataContext;

        public SocietyTypeRepository(HCMKomatsuProdContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public ResultDto CreatedOrUpdate(TipoSociedad entity)
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

        public override TipoSociedad GetById(int id)
        {
            return _dataContext.TipoSociedad.FirstOrDefault(x => x.Id == id);
        }

        public TipoSociedad GetByName(string name)
        {
            var cleanName = Utils.Utils.CleanString(name).ToUpper();

            return _dataContext.TipoSociedad.AsEnumerable().FirstOrDefault(s => Utils.Utils.CleanString(s.Nombre).ToUpper() == cleanName);
        }
    }
}
