using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd
{
    public partial class UsersPerfil
    {
        public int Id { get; set; }
        public int? IdUsers { get; set; }
        public int? IdPerfil { get; set; }
        public bool? Activo { get; set; }

        public virtual Perfil IdPerfilNavigation { get; set; }
        public virtual Users IdUsersNavigation { get; set; }
    }
}
