using DigitalLearningDataImporter.DALstd.ProdEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.Prod.Dto
{
    public class LocalDto
    {
        public int Id { get; set; }
        public string CodigoLocal { get; set; }
        public string NombreLocal { get; set; }
        public bool Activo { get; set; }
        public int? IdFormato { get; set; }

        public LocalDto()
        {

        }

        public LocalDto(Locales loc)
        {
            Id = loc.Id;
            CodigoLocal = loc.CodigoLocal;
            NombreLocal = loc.NombreLocal;
            Activo = loc.Activo;
            //IdFormato = loc.IdFormato;
        }
    }
}
