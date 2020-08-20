using DigitalLearningDataImporter.DALstd.ProdEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.Prod.Dto
{
    public class WorkingDayDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool? Activo { get; set; }
        public WorkingDayDto(JornadaLaboral jornadaLaboral)
        {
            Id = jornadaLaboral.Id;
            Nombre = jornadaLaboral.Nombre;
            Descripcion = jornadaLaboral.Descripcion;
            Activo = jornadaLaboral.Activo;
        }
    }
}
