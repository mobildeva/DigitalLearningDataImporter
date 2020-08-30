using DigitalLearningDataImporter.DALstd.ProdEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.Prod.Dto
{
    public class AreaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }
        public string UsuarioCr { get; set; }
        public DateTime FechaCr { get; set; }
        public string UsuarioUp { get; set; }
        public DateTime? FechaUp { get; set; }

        public AreaDto(Area a)
        {
            Id = a.Id;
            Nombre = a.Nombre;
            Activo = a.Activo;
            UsuarioCr = a.UsuarioCr;
            FechaCr = a.FechaCr;
            UsuarioUp = a.UsuarioUp;
            FechaUp = a.FechaUp;
        }

        public AreaDto()
        {

        }
    }
}
