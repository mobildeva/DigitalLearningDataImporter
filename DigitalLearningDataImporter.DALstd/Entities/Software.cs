using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd
{
    public partial class Software
    {
        public Software()
        {
            ClienteSoftware = new HashSet<ClienteSoftware>();
            SoftwareModulo = new HashSet<SoftwareModulo>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<ClienteSoftware> ClienteSoftware { get; set; }
        public virtual ICollection<SoftwareModulo> SoftwareModulo { get; set; }
    }
}
