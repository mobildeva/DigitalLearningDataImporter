using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd
{
    public partial class SoftwareModulo
    {
        public SoftwareModulo()
        {
            ClientesSoftwareModulos = new HashSet<ClientesSoftwareModulos>();
            Paginas = new HashSet<Paginas>();
        }

        public int Id { get; set; }
        public int? IdSoftware { get; set; }
        public int? IdModulos { get; set; }
        public DateTime? Fecha { get; set; }
        public bool? Activo { get; set; }

        public virtual Modulos IdModulosNavigation { get; set; }
        public virtual Software IdSoftwareNavigation { get; set; }
        public virtual ICollection<ClientesSoftwareModulos> ClientesSoftwareModulos { get; set; }
        public virtual ICollection<Paginas> Paginas { get; set; }
    }
}
