using DigitalLearningDataImporter.DALstd.ProdEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.Prod.Dto
{
    public class OcupLevelDto
    {
        public int Id { get; set; }
        public int? IdSociedad { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }
        public OcupLevelDto()
        {

        }
        public OcupLevelDto(NivelOcupacional nivelOcupacional)
        {
            Id = nivelOcupacional.Id;
            IdSociedad = nivelOcupacional.IdSociedad;
            Nombre = nivelOcupacional.Nombre;
            Activo = nivelOcupacional.Activo;
        }
    }
}
