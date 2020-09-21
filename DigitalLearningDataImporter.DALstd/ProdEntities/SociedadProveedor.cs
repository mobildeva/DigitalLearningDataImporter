using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd.ProdEntities
{
    public partial class SociedadProveedor
    {
        public int Id { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdTipoSociedad { get; set; }
        public int? IdSociedad { get; set; }
        public bool? Activo { get; set; }

        public virtual Sociedad IdSociedadNavigation { get; set; }
        public virtual TipoSociedad IdTipoSociedadNavigation { get; set; }
    }
}
