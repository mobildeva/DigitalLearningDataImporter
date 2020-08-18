using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd.ProdEntities
{
    public partial class Personas
    {
        public Personas()
        {
            UnidadesNegocio = new HashSet<UnidadesNegocio>();
        }

        public int Id { get; set; }
        public string IdentificacionUnica { get; set; }
        public string Dv { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Email { get; set; }
        public int? IdCodigoArea { get; set; }
        public string Fono { get; set; }
        public string Celular { get; set; }
        public int? IdConexion { get; set; }
        public string ClaveSence { get; set; }
        public bool? Activo { get; set; }
        public bool? ConectaSence { get; set; }
        public bool? Instructor { get; set; }
        public int? IdPersonaForo { get; set; }

        public virtual ICollection<UnidadesNegocio> UnidadesNegocio { get; set; }
    }
}
