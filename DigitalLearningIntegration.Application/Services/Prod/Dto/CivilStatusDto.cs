using DigitalLearningDataImporter.DALstd.ProdEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.Prod.Dto
{
    public class CivilStatusDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }
        public CivilStatusDto(EstadoCivil ec)
        {
            Id = ec.Id;
            Nombre = ec.Nombre;
            Activo = ec.Activo;
        }
    }
}
