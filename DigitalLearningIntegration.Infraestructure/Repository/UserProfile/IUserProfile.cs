using DigitalLearningDataImporter.DALstd;
using DigitalLearningIntegration.Infraestructure.Dto;
using DigitalLearningIntegration.Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Infraestructure.Repository.UserProfile
{
    public interface IUserProfile : IRepository<UsersPerfil>
    {
        UsersPerfil GetUserByUserIdAndPerfilId(int userId, int profileId);
        ResultDto CreatedOrUpdate(UsersPerfil entity);
        UsersPerfil GetUserByUserId(int userId);
    }
}
