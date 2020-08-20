using DigitalLearningDataImporter.DALstd.ProdEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.Prod.Dto
{
    public class PeoplesDto
    {
        public int Id { get; set; }
        public string IdentificacionUnica { get; set; }
        public string Dv { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Email { get; set; }
        public int? IdCodigoArea { get; set; }
        public string Fono { get; set; }
        public string Celular { get; set; }
        public int? IdConexion { get; set; }
        public string ClaveSence { get; set; }
        public bool? Activo { get; set; }
        public bool? ConectaSence { get; set; }
        public bool? Instructor { get; set; }
        public int? IdPersonaForo { get; set; }
        public PeoplesDto(Personas p)
        {
            Id = p.Id;
            IdentificacionUnica = p.IdentificacionUnica;
            Dv = p.Dv;
            Nombre = p.Nombre;
            ApellidoMaterno = p.ApellidoMaterno;
            ApellidoPaterno = p.ApellidoPaterno;
            Email = p.Email;
            IdCodigoArea = p.IdCodigoArea;
            Fono = p.Fono;
            Celular = p.Celular;
            IdConexion = p.IdConexion;
            ClaveSence = p.ClaveSence;
            Activo = p.Activo;
            ConectaSence = p.ConectaSence;
            Instructor = p.Instructor;
            IdPersonaForo = p.IdPersonaForo;
        }
    }
}
