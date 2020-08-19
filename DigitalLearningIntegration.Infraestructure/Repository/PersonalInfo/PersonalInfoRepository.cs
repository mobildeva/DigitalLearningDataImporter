using DigitalLearningDataImporter.DALstd.ProdEntities;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitalLearningIntegration.Infraestructure.Repository.PersonalInfo
{
    public class PersonalInfoRepository : Repository<InformacionPersonal>, IPersonalInfoRepository
    {
        private readonly HCMKomatsuProdContext _dataContext;

        public PersonalInfoRepository(HCMKomatsuProdContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public ResultDto CreatedOrUpdate(InformacionPersonal entity)
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

        public IEnumerable<InformacionPersonal> GetAll()
        {
            return base.GetAll();
        }

        public override InformacionPersonal GetById(int id)
        {
            return _dataContext.InformacionPersonal.FirstOrDefault(x => x.Id == id);
        }
    }
}
