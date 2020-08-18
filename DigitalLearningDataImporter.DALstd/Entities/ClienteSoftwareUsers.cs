using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd
{
    public partial class ClienteSoftwareUsers
    {
        public ClienteSoftwareUsers()
        {
            UsuarioPerfil = new HashSet<UsuarioPerfil>();
        }

        public int Id { get; set; }
        public int? IdUsers { get; set; }
        public int? IdClientesSoftwareModulos { get; set; }
        public bool? Activo { get; set; }

        public virtual Users IdUsersNavigation { get; set; }
        public virtual ICollection<UsuarioPerfil> UsuarioPerfil { get; set; }
    }
}
