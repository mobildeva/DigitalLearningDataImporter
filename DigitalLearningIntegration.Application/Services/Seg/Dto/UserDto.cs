using System;
using System.Collections.Generic;
using System.Text;
using DigitalLearningDataImporter.DALstd;

namespace DigitalLearningIntegration.Application.Services.Seg.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? Fecha { get; set; }
        public bool? PrimerIngreso { get; set; }
        public bool? Activo { get; set; }
        public DateTime? FechaUltimoIntento { get; set; }
        public int? NumeroIntentosFallidos { get; set; }
        public bool? Bloqueado { get; set; }
        public string Token { get; set; }
        public DateTime? FechaToken { get; set; }
        public UserDto(Users user)
        {
            Id = user.Id;
            Nombres = user.Nombres;
            Username = user.Username;
            Password = user.Password;
            Fecha = user.Fecha;
            PrimerIngreso = user.PrimerIngreso;
            Activo = user.Activo;
            FechaUltimoIntento = user.FechaUltimoIntento;
            NumeroIntentosFallidos = user.NumeroIntentosFallidos;
            Bloqueado = user.Bloqueado;
            Token = user.Token;
            FechaToken = user.FechaToken;
        }
        public UserDto()
        {

        }
    }
}
