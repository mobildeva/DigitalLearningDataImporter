using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd
{
    public partial class ModuloPaginaFuncionalidad
    {
        public int Id { get; set; }
        public int? IdPaginas { get; set; }
        public int? IdFuncionalidades { get; set; }
        public int? IdPerfil { get; set; }
        public string Nombre { get; set; }
        public bool? Estado { get; set; }
        public bool? Activo { get; set; }

        public virtual Funcionalidades IdFuncionalidadesNavigation { get; set; }
        public virtual Paginas IdPaginasNavigation { get; set; }
        public virtual Perfil IdPerfilNavigation { get; set; }
    }
}
