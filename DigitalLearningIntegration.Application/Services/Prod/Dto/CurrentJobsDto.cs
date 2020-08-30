using DigitalLearningDataImporter.DALstd.ProdEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.Prod.Dto
{
    public class CurrentJobsDto
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

        public CurrentJobsDto(PosicionLaboral posicionLaboral)
        {
            Id = posicionLaboral.Id;
            IdPersona = posicionLaboral.IdPersona;
            IdSociedad = posicionLaboral.IdSociedad;
            IdUnidadOrganizacional = posicionLaboral.IdUnidadOrganizacional;
            IdUnidadNegocio = posicionLaboral.IdUnidadNegocio;
            IdCargo = posicionLaboral.IdCargo;
            IdEscolaridadSence = posicionLaboral.IdEscolaridadSence;
            IdPersonaJefe = posicionLaboral.IdPersonaJefe;
            FranquiciaSence = posicionLaboral.FranquiciaSence;
            IdUbicacion = posicionLaboral.IdUbicacion;
            IdSociedadContratante = posicionLaboral.IdSociedadContratante;
            IdTipoContrato = posicionLaboral.IdTipoContrato;
            FechaInicioContrato = posicionLaboral.FechaInicioContrato;
            FechaTerminoContrato = posicionLaboral.FechaTerminoContrato;
            IdNivelOcupacional = posicionLaboral.IdNivelOcupacional;
            Activo = posicionLaboral.Activo;
            IdCentroCosto = posicionLaboral.IdCentroCosto;
            Estado = posicionLaboral.Estado;
            FechaInicioPosicion = posicionLaboral.FechaInicioPosicion;
            FechaTerminoPosicion = posicionLaboral.FechaTerminoPosicion;
            ComentarioDesvinculacion = posicionLaboral.ComentarioDesvinculacion;
            NombrePosicion = posicionLaboral.NombrePosicion;
            NombrePosicionAnterior = posicionLaboral.NombrePosicionAnterior;
            IdPersonaCambio = posicionLaboral.IdPersonaCambio;
            IdTipoCambioPosicion = posicionLaboral.IdTipoCambioPosicion;
            IdTipoPosicion = posicionLaboral.IdTipoPosicion;
            IdSociedadContratante = posicionLaboral.IdSociedadContratante;
        }
        public CurrentJobsDto()
        {

        }
    }
}
