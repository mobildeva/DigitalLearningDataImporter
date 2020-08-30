using DigitalLearningDataImporter.DALstd;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DigitalLearningIntegration.Infraestructure.Dto;

namespace DigitalLearningIntegration.Infraestructure.Repository.UserProfile
{
    public class UserProfile : Repository<UsersPerfil>, IUserProfile
    {
        private readonly HCMKomatsuSegContext _context;

        public UserProfile(HCMKomatsuSegContext dataContext) : base(dataContext)
        {
            _context = dataContext;
        }

        public ResultDto CreatedOrUpdate(UsersPerfil entity)
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

        public override UsersPerfil GetById(int id)
        {
            return _context.UsersPerfil.FirstOrDefault(x => x.Id == id);
        }

        public UsersPerfil GetUserByUserIdAndPerfilId(int userId, int profileId)
        {
            return _context.UsersPerfil.AsEnumerable().FirstOrDefault(u => u.IdUsers == userId && u.IdPerfil == profileId);
        }
    }
}
