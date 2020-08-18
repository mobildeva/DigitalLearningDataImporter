using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd
{
    public partial class Paginas
    {
        public Paginas()
        {
            InverseIdPadreNavigation = new HashSet<Paginas>();
            ModuloPaginaFuncionalidad = new HashSet<ModuloPaginaFuncionalidad>();
            Perfil = new HashSet<Perfil>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdSoftwareModulo { get; set; }
        public int? IdPadre { get; set; }
        public string Pagina { get; set; }
        public int? Orden { get; set; }
        public bool? Orientacion { get; set; }
        public bool? Activo { get; set; }
        public bool AbrirEnNuevaPestana { get; set; }

        public virtual Paginas IdPadreNavigation { get; set; }
        public virtual SoftwareModulo IdSoftwareModuloNavigation { get; set; }
        public virtual ICollection<Paginas> InverseIdPadreNavigation { get; set; }
        public virtual ICollection<ModuloPaginaFuncionalidad> ModuloPaginaFuncionalidad { get; set; }
        public virtual ICollection<Perfil> Perfil { get; set; }
    }
}
