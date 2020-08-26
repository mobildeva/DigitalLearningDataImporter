using DigitalLearningDataImporter.DALstd;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalLearningIntegration.Infraestructure.Repository.Clients
{
    public class ClientRepository : Repository<Clientes>, IClientRepository
    {
        private readonly HCMKomatsuSegContext _dataContext;

        public ClientRepository(HCMKomatsuSegContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public ResultDto CreatedOrUpdate(Clientes entity)
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

        public IEnumerable<Clientes> GetAll()
        {
            return base.GetAll();
        }

        public override Clientes GetById(int id)
        {
            return _dataContext.Clientes.FirstOrDefault(c => c.Id == id);
        }

        public Clientes GetClientBySocietyId(int societyId)
        {
            return _dataContext.Clientes.FirstOrDefault(c => c.IdSociedad == societyId);
        }
    }
}
