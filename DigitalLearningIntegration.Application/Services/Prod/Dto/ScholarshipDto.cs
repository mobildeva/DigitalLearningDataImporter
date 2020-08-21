using DigitalLearningDataImporter.DALstd.ProdEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.Prod.Dto
{
    public class ScholarshipDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }
        public ScholarshipDto()
        {

        }
        public ScholarshipDto(EscolaridadSence es)
        {
            Id = es.Id;
            Nombre = es.Nombre;
            Activo = es.Activo;
        }
    }
}
