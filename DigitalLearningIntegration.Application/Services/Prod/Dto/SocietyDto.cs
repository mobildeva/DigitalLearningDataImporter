using DigitalLearningDataImporter.DALstd.ProdEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.Prod.Dto
{
    public class SocietyDto
    {
        public int Id { get; set; }
        public string IdentificacionUnica { get; set; }
        public string Nombre { get; set; }
        public string SiglaSociedad { get; set; }
        public string NombreContacto { get; set; }
        public string CorreoContacto { get; set; }
        public string Fono { get; set; }
        public string Fax { get; set; }
        public string Logo { get; set; }
        public string CodErp { get; set; }
        public string Portal { get; set; }
        public string Skin { get; set; }
        public string ClaveSence { get; set; }
        public bool? Activo { get; set; }
        public string Direccion { get; set; }
        public int? IdUbicacion { get; set; }

        public SocietyDto(Sociedad s)
        {
            Id = s.Id;
            IdentificacionUnica = s.IdentificacionUnica;
            Nombre = s.Nombre;
            Activo = s.Activo;
        }
    }
}
