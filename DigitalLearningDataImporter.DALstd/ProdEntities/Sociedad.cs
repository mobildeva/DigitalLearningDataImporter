using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd.ProdEntities
{
    public partial class Sociedad
    {
        public Sociedad()
        {
            CentroCosto = new HashSet<CentroCosto>();
            UnidadesOrganizacional = new HashSet<UnidadesOrganizacional>();
            Cargos = new HashSet<Cargos>();
            FamiliaCargo = new HashSet<FamiliaCargo>();
        }

        public int Id { get; set; }
        public string IdentificacionUnica { get; set; }
        public string Nombre { get; set; }
        public string SiglaSociedad { get; set; }
        public string NombreContacto { get; set; }
        public string CorreoContacto { get; set; }
        public string Fono { get; set; }
        public string Fax { get; set; }
        public string Logo { get; set; }
        public string CodErp { get; set; }
        public string Portal { get; set; }
        public string Skin { get; set; }
        public string ClaveSence { get; set; }
        public bool? Activo { get; set; }
        public string Direccion { get; set; }
        public int? IdUbicacion { get; set; }

        public virtual Ubicacion IdUbicacionNavigation { get; set; }
        public virtual ICollection<CentroCosto> CentroCosto { get; set; }
        public virtual ICollection<UnidadesOrganizacional> UnidadesOrganizacional { get; set; }
        public virtual ICollection<UnidadesNegocio> UnidadesNegocio { get; set; }
        public virtual ICollection<Cargos> Cargos { get; set; }
        public virtual ICollection<FamiliaCargo> FamiliaCargo { get; set; }
    }
}
