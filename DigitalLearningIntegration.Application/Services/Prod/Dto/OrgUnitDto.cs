using DigitalLearningDataImporter.DALstd.ProdEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.Prod.Dto
{
    public class OrgUnitDto
    {
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
        public OrgUnitDto(UnidadesOrganizacional uo)
        {
            Id = uo.Id;
            IdSociedad = uo.IdSociedad;
            Nombre = uo.Nombre;
            IdPadre = uo.IdPadre;
            IdCentroCosto = uo.IdCentroCosto;
            IdPersonas = uo.IdPersonas;
            IdUbicacionesFisica = uo.IdUbicacionesFisica;
            CodigoErp = uo.CodigoErp;
            Activo = uo.Activo;
            Nivel = uo.Nivel;
        }
    }
}
