using DigitalLearningDataImporter.DALstd;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.Seg.Dto
{
    public class UserProfileDto
    {
        public int Id { get; set; }
        public int? IdUsers { get; set; }
        public int? IdPerfil { get; set; }
        public bool? Activo { get; set; }
        public UserProfileDto()
        {

        }
        public UserProfileDto(UsersPerfil usersPerfil)
        {
            Id = usersPerfil.Id;
            IdUsers = usersPerfil.IdUsers;
            IdPerfil = usersPerfil.IdPerfil;
            Activo = usersPerfil.Activo;
        }
    }
}
