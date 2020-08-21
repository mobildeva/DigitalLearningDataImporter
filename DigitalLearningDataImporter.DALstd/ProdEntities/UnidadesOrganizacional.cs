using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd.ProdEntities
{
    public partial class UnidadesOrganizacional
    {
        public UnidadesOrganizacional()
        {
            Cargos = new HashSet<Cargos>();
            InverseIdPadreNavigation = new HashSet<UnidadesOrganizacional>();
            UnidadesNegocio = new HashSet<UnidadesNegocio>();
        }

        public int Id { get; set; }
        public int IdSociedad { get; set; }
        public string Nombre { get; set; }
        public int? IdPadre { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdPersonas { get; set; }
        public int? IdUbicacionesFisica { get; set; }
        public string CodigoErp { get; set; }
        public bool? Activo { get; set; }
        public int? Nivel { get; set; }

        public virtual UnidadesOrganizacional IdPadreNavigation { get; set; }
        public virtual ICollection<UnidadesOrganizacional> InverseIdPadreNavigation { get; set; }
        public virtual ICollection<UnidadesNegocio> UnidadesNegocio { get; set; }
        public virtual ICollection<Cargos> Cargos { get; set; }
        public virtual Sociedad IdSociedadNavigation { get; set; }
        public virtual CentroCosto IdCentroCostoNavigation { get; set; }
    }
}
