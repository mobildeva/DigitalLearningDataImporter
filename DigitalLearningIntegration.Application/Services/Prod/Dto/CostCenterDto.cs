using DigitalLearningDataImporter.DALstd.ProdEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.Prod.Dto
{
    public class CostCenterDto
    {
        public int Id { get; set; }
        public int IdSociedad { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }
        public CostCenterDto()
        {

        }
        public CostCenterDto(CentroCosto cc)
        {
            Id = cc.Id;
            IdSociedad = cc.IdSociedad;
            Nombre = cc.Nombre;
            Activo = cc.Activo;
            Codigo = cc.Codigo;
        }
    }
}
