using DigitalLearningDataImporter.DALstd;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLearningIntegration.Application.Services.Seg.Dto
{
    public class ClienteUsersDto
    {
        public int Id { get; set; }
        public int? IdUsers { get; set; }
        public int? IdClientes { get; set; }
        public bool? Activo { get; set; }

        public ClienteUsersDto()
        {

        }

        public ClienteUsersDto(ClienteUsers clienteUsers)
        {
            Id = clienteUsers.Id;
            IdUsers = clienteUsers.IdUsers;
            IdClientes = clienteUsers.IdClientes;
            Activo = clienteUsers.Activo;
        }
    }
}
