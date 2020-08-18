using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd
{
    public partial class ClienteSoftware
    {
        public ClienteSoftware()
        {
            ClientesSoftwareModulos = new HashSet<ClientesSoftwareModulos>();
        }

        public int Id { get; set; }
        public int? IdSoftware { get; set; }
        public int? IdClientes { get; set; }
        public DateTime? Fecha { get; set; }
        public string Imagen { get; set; }
        public string Url { get; set; }
        public bool? Activo { get; set; }

        public virtual Clientes IdClientesNavigation { get; set; }
        public virtual Software IdSoftwareNavigation { get; set; }
        public virtual ICollection<ClientesSoftwareModulos> ClientesSoftwareModulos { get; set; }
    }
}
