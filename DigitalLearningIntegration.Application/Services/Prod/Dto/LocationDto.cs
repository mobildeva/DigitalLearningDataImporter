using DigitalLearningDataImporter.DALstd.ProdEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.Prod.Dto
{
    public class LocationDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public short Orden { get; set; }
        public int? IdPadre { get; set; }
        public byte? IdTipoUbicacion { get; set; }
        public string CodigoArea { get; set; }
        public bool? Activo { get; set; }
        public int CodigoUbicacionSence { get; set; }
        public LocationDto()
        {

        }
        public LocationDto(Ubicacion ub)
        {
            Id = ub.Id;
            Nombre = ub.Nombre;
            Orden = ub.Orden;
            IdPadre = ub.IdPadre;
            IdTipoUbicacion = ub.IdTipoUbicacion;
            CodigoArea = ub.CodigoArea;
            Activo = ub.Activo;
            CodigoUbicacionSence = ub.CodigoUbicacionSence;
        }
    }
}
