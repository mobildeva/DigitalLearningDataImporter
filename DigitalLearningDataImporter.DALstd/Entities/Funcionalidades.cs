using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd
{
    public partial class Funcionalidades
    {
        public Funcionalidades()
        {
            ModuloPaginaFuncionalidad = new HashSet<ModuloPaginaFuncionalidad>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime? Fecha { get; set; }
        public string Tag { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<ModuloPaginaFuncionalidad> ModuloPaginaFuncionalidad { get; set; }
    }
}
