using DigitalLearningDataImporter.DALstd.ProdEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.Prod.Dto
{
    public class FamilyDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? IdSociedad { get; set; }
        public bool? Activo { get; set; }
        public FamilyDto(FamiliaCargo familiaCargo)
        {
            Id = familiaCargo.Id;
            Nombre = familiaCargo.Nombre;
            IdSociedad = familiaCargo.IdSociedad;
            Activo = familiaCargo.Activo;
            Descripcion = familiaCargo.Descripcion;
        }

        public FamilyDto()
        {

        }
    }
}
