using DigitalLearningDataImporter.DALstd.ProdEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.Prod.Dto
{
    public class AfpDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }

        public AfpDto(Afp afp)
        {
            Id = afp.Id;
            Nombre = afp.Nombre;
            Activo = afp.Activo;
        }
    }
}
