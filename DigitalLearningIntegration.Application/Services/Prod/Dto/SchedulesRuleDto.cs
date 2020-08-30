using DigitalLearningDataImporter.DALstd.ProdEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.Prod.Dto
{
    public class SchedulesRuleDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }
        public string UsuarioCr { get; set; }
        public DateTime FechaCr { get; set; }
        public string UsuarioUp { get; set; }
        public DateTime? FechaUp { get; set; }
        public SchedulesRuleDto(ReglaPlanHorario reglaPlan)
        {
            Id = reglaPlan.Id;
            Nombre = reglaPlan.Nombre;
            Activo = reglaPlan.Activo;
            UsuarioCr = reglaPlan.UsuarioCr;
            FechaCr = reglaPlan.FechaCr;
            UsuarioUp = reglaPlan.UsuarioUp;
            FechaUp = reglaPlan.FechaUp;
        }

        public SchedulesRuleDto()
        {

        }
    }
}
