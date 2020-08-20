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
    public class SocietyRepository : Repository<Sociedad>, ISocietyRepository
    {
        private readonly HCMKomatsuProdContext _dataContext;

        public SocietyRepository(HCMKomatsuProdContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public ResultDto CreatedOrUpdate(Sociedad entity)
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

        public IEnumerable<Sociedad> GetAll()
        {
            return base.GetAll();
        }

        public override Sociedad GetById(int id)
        {
            return _dataContext.Sociedad.FirstOrDefault(x => x.Id == id);
        }

        public Sociedad GetByName(string name)
        {
            return _dataContext.Sociedad.AsEnumerable().FirstOrDefault(s => Utils.Utils.CleanString(s.Nombre).ToUpper() == Utils.Utils.CleanString(name).ToUpper());
        }

        public Sociedad GetByUniqueIdentifier(string uniqIdent)
        {
            return _dataContext.Sociedad.AsEnumerable().FirstOrDefault(s => Utils.Utils.CleanString(s.IdentificacionUnica).ToUpper() == Utils.Utils.CleanString(uniqIdent).ToUpper());
        }
    }
}
