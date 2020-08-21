using DigitalLearningDataImporter.DALstd.ProdEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.Prod.Dto
{
    public class JobDto
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
        public JobDto()
        {

        }
        public JobDto(Cargos cargo)
        {
            Id = cargo.Id;
            Nombre = cargo.Nombre;
            Activo = Activo;
            IdSociedad = cargo.IdSociedad;
            IdFamiliaCargo = cargo.IdFamiliaCargo;
            Descripcion = cargo.Descripcion;
            IdEscalaSalarial = cargo.IdEscalaSalarial;
            IdEspecialidadCargo = cargo.IdEspecialidadCargo;
            CodigoErp = cargo.CodigoErp;
            Anexo = cargo.Anexo;
            IdJornadaLaboral = cargo.IdJornadaLaboral;
            IdUnidadOrganizacional = cargo.IdUnidadOrganizacional;
            IdUbicacionesFisicas = cargo.IdUbicacionesFisicas;
            FechaCreacion = cargo.FechaCreacion;
            FechaModificacion = cargo.FechaModificacion;
            Supervision = cargo.Supervision;
            Usuariocreacion = cargo.Usuariocreacion;
            Usuariomodificacion = cargo.Usuariomodificacion;
        }
    }
}
