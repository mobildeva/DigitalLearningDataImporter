using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd
{
    public partial class UsuarioPerfil
    {
        public int Id { get; set; }
        public int? IdClienteSoftwareUsers { get; set; }
        public int? IdPerfil { get; set; }
        public bool? Activo { get; set; }

        public virtual ClienteSoftwareUsers IdClienteSoftwareUsersNavigation { get; set; }
        public virtual Perfil IdPerfilNavigation { get; set; }
    }
}
