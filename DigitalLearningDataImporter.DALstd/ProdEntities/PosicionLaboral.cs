using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd.ProdEntities
{
    public partial class PosicionLaboral
    {
        public int Id { get; set; }
        public int IdPersona { get; set; }
        public int? IdSociedad { get; set; }
        public int? IdUnidadOrganizacional { get; set; }
        public int? IdUnidadNegocio { get; set; }
        public int? IdCargo { get; set; }
        public int? IdEscolaridadSence { get; set; }
        public int? IdTipoPosicion { get; set; }
        public int? IdTipoCambioPosicion { get; set; }
        public string NombrePosicion { get; set; }
        public string NombrePosicionAnterior { get; set; }
        public string SociedadAnterior { get; set; }
        public int? IdPersonaJefe { get; set; }
        public string FranquiciaSence { get; set; }
        public int? IdUbicacion { get; set; }
        public DateTime? FechaInicioPosicion { get; set; }
        public DateTime? FechaTerminoPosicion { get; set; }
        public byte Estado { get; set; }
        public int? IdPosicionOrigen { get; set; }
        public int? IdSociedadContratante { get; set; }
        public int? IdTipoTerminoContrato { get; set; }
        public int? IdTipoContrato { get; set; }
        public DateTime? FechaInicioContrato { get; set; }
        public DateTime? FechaTerminoContrato { get; set; }
        public string ComentarioDesvinculacion { get; set; }
        public int? IdNivelOcupacional { get; set; }
        public bool? Activo { get; set; }
        public int? IdPersonaCambio { get; set; }
        public int IdCentroCosto { get; set; }

        public virtual EscolaridadSence IdEscolaridadSenceNavigation { get; set; }
        public virtual TipoContrato IdTipoContratoNavigation { get; set; }
        public virtual Ubicacion IdUbicacionNavigation { get; set; }
        public virtual Personas IdPersonaCambioNavigation { get; set; }
        public virtual Personas IdPersonaJefeNavigation { get; set; }
        public virtual Personas IdPersonaNavigation { get; set; }
        public virtual UnidadesNegocio IdUnidadNegocioNavigation { get; set; }
    }
}
