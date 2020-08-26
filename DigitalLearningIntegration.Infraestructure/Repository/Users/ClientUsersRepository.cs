using DigitalLearningDataImporter.DALstd;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitalLearningIntegration.Infraestructure.Repository.Users
{
    public class ClientUsersRepository : Repository<ClienteUsers>, IClientUsersRepository
    {
        private readonly HCMKomatsuSegContext _context;

        public ClientUsersRepository(HCMKomatsuSegContext dataContext) : base(dataContext)
        {
            _context = dataContext;
        }

        public ResultDto CreatedOrUpdate(ClienteUsers entity)
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

        public override ClienteUsers GetById(int id)
        {
            return _context.ClienteUsers.FirstOrDefault(x => x.Id == id);
        }

        public ClienteUsers GetClientUsersByClientUserId(int? idClientes, int? idUser)
        {
            return _context.ClienteUsers.FirstOrDefault(cu => cu.IdClientes == idClientes && cu.IdUsers == idUser);
        }

        public IEnumerable<ClienteUsers> GetUsersByClientId(int clientId)
        {
            return _context.ClienteUsers.Where(cu => cu.IdClientes == clientId);
        }
    }
}
