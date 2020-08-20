using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd.ProdEntities
{
    public partial class CentroCosto
    {
        public CentroCosto()
        {
            UnidadesOrganizacional = new HashSet<UnidadesOrganizacional>();
        }

        public int Id { get; set; }
        public int IdSociedad { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }

        public virtual Sociedad IdSociedadNavigation { get; set; }
        public virtual ICollection<UnidadesOrganizacional> UnidadesOrganizacional { get; set; }
    }
}
