using DigitalLearningDataImporter.DALstd.ProdEntities;
using DocumentFormat.OpenXml.Wordprocessing;
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

        public SocietyDto()
        {

        }
        public SocietyDto(Sociedad s)
        {
            Id = s.Id;
            IdentificacionUnica = s.IdentificacionUnica;
            Nombre = s.Nombre;
            NombreContacto = s.NombreContacto;
            CorreoContacto = s.CorreoContacto;
            Fono = s.Fono;
            Fax = s.Fax;
            Logo = s.Logo;
            CodErp = s.CodErp;
            Portal = s.Portal;
            Skin = s.Skin;
            ClaveSence = s.ClaveSence;
            Direccion = s.Direccion;
            IdUbicacion = s.IdUbicacion;
            Activo = s.Activo;
            SiglaSociedad = s.SiglaSociedad;
        }
    }

    public class ProvSocietyDto
    {
        public int Id { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdTipoSociedad { get; set; }
        public int? IdSociedad { get; set; }
        public bool? Activo { get; set; }
        public ProvSocietyDto(SociedadProveedor sociedadProveedor)
        {
            Id = sociedadProveedor.Id;
            IdProveedor = sociedadProveedor.IdProveedor;
            IdSociedad = sociedadProveedor.IdSociedad;
            IdTipoSociedad = sociedadProveedor.IdTipoSociedad;
        }

        public ProvSocietyDto()
        {

        }
    }

    public class SocietyTypeDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }
        public SocietyTypeDto(TipoSociedad tipoSociedad)
        {
            Id = tipoSociedad.Id;
            Nombre = tipoSociedad.Nombre;
            Activo = tipoSociedad.Activo;
        }

        public SocietyTypeDto()
        {

        }
    }
}
