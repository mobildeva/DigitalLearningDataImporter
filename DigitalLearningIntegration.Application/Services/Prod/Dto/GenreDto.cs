using DigitalLearningDataImporter.DALstd.ProdEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.Prod.Dto
{
    public class GenreDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }
        public GenreDto(Genero gen)
        {
            Id = gen.Id;
            Nombre = gen.Nombre;
            Activo = gen.Activo;
        }
    }
}
