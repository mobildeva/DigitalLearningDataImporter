using DigitalLearningDataImporter.DALstd.ProdEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.Prod.Dto
{
    public class ContractTypeDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }
        public ContractTypeDto(TipoContrato tipoContrato)
        {
            Id = tipoContrato.Id;
            Nombre = tipoContrato.Nombre;
            Activo = tipoContrato.Activo;
        }
    }
}
