using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd.ProdEntities
{
    public partial class TipoSociedad
    {
        public TipoSociedad()
        {
            SociedadProveedor = new HashSet<SociedadProveedor>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<SociedadProveedor> SociedadProveedor { get; set; }
    }
}
