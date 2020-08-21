using DigitalLearningDataImporter.DALstd.ProdEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.Prod.Dto
{
    public class CountryDto
    {
        public int IdPais { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }
        public string CodigoArea { get; set; }
        public int CodigoSence { get; set; }
        public CountryDto(Pais pais)
        {
            IdPais = pais.IdPais;
            Nombre = pais.Nombre;
            Activo = pais.Activo;
            CodigoArea = pais.CodigoArea;
            CodigoSence = pais.CodigoSence;
        }

        public CountryDto()
        {

        }
    }
}
