using DigitalLearningDataImporter.DALstd;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.Seg.Dto
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdSociedad { get; set; }
        public bool? Activo { get; set; }

        public ClientDto()
        {

        }

        public ClientDto(Clientes cliente)
        {
            Id = cliente.Id;
            IdSociedad = cliente.IdSociedad;
            Nombre = cliente.Nombre;
            Activo = cliente.Activo;
        }
    }
}
