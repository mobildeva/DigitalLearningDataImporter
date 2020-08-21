using DigitalLearningDataImporter.DALstd.ProdEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.Prod.Dto
{
    public class BloodGDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public BloodGDto(GrupoSanguineo gs)
        {
            Id = gs.Id;
            Nombre = gs.Nombre;
            Activo = gs.Activo;
        }

        public BloodGDto()
        {

        }
    }
}
