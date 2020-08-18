using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd
{
    public partial class ClientesSoftwareModulos
    {
        public int Id { get; set; }
        public int? IdClienteSoftware { get; set; }
        public int? IdSoftwareModulo { get; set; }
        public bool? Activo { get; set; }

        public virtual ClienteSoftware IdClienteSoftwareNavigation { get; set; }
        public virtual SoftwareModulo IdSoftwareModuloNavigation { get; set; }
    }
}
