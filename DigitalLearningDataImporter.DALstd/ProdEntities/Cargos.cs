using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd.ProdEntities
{
    public partial class Cargos
    {
        public int Id { get; set; }
        public int IdSociedad { get; set; }
        public int? IdFamiliaCargo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? IdEscalaSalarial { get; set; }
        public int? IdEspecialidadCargo { get; set; }
        public string CodigoErp { get; set; }
        public string Anexo { get; set; }
        public int? IdJornadaLaboral { get; set; }
        public int? IdUnidadOrganizacional { get; set; }
        public int? IdUbicacionesFisicas { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public bool? Supervision { get; set; }
        public string Usuariocreacion { get; set; }
        public string Usuariomodificacion { get; set; }
        public bool? Activo { get; set; }

        public virtual FamiliaCargo IdFamiliaCargoNavigation { get; set; }
        public virtual Sociedad IdSociedadNavigation { get; set; }
        public virtual UnidadesOrganizacional IdUnidadOrganizacionalNavigation { get; set; }
    }
}
