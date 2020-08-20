using DigitalLearningDataImporter.DALstd.ProdEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.Prod.Dto
{
    public class BussUnitDto
    {
        public int Id { get; set; }
        public int IdSociedad { get; set; }
        public string Nombre { get; set; }
        public int? IdUnidadOrganizacional { get; set; }
        public int? IdPersonaJefe { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdUbicacionFisica { get; set; }
        public string CodigoErp { get; set; }
        public bool? Activo { get; set; }

        public BussUnitDto(UnidadesNegocio un)
        {
            Id = un.Id;
            IdSociedad = un.IdSociedad;
            Nombre = un.Nombre;
            Activo = un.Activo;
            IdCentroCosto = un.IdCentroCosto;
        }
    }
}
