using DigitalLearningDataImporter.DALstd.ProdEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.Prod.Dto
{
    public class IsapreDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }
        public IsapreDto(Isapres isapre)
        {
            Id = isapre.Id;
            Nombre = isapre.Nombre;
            Activo = isapre.Activo;
        }
    }
}
