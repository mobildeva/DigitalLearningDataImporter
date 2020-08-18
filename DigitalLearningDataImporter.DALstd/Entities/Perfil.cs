using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd
{
    public partial class Perfil
    {
        public Perfil()
        {
            ModuloPaginaFuncionalidad = new HashSet<ModuloPaginaFuncionalidad>();
            UsersPerfil = new HashSet<UsersPerfil>();
            UsuarioPerfil = new HashSet<UsuarioPerfil>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime? Fecha { get; set; }
        public string Descripcion { get; set; }
        public int? IdSociedad { get; set; }
        public bool? Activo { get; set; }
        public int? IdPaginaInicio { get; set; }
        public bool IsModerador { get; set; }

        public virtual Paginas IdPaginaInicioNavigation { get; set; }
        public virtual ICollection<ModuloPaginaFuncionalidad> ModuloPaginaFuncionalidad { get; set; }
        public virtual ICollection<UsersPerfil> UsersPerfil { get; set; }
        public virtual ICollection<UsuarioPerfil> UsuarioPerfil { get; set; }
    }
}
