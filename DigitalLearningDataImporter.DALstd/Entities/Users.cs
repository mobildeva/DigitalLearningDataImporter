using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd
{
    public partial class Users
    {
        public Users()
        {
            ClienteSoftwareUsers = new HashSet<ClienteSoftwareUsers>();
            ClienteUsers = new HashSet<ClienteUsers>();
            UsersPerfil = new HashSet<UsersPerfil>();
            WidgetUsers = new HashSet<WidgetUsers>();
        }

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

        public virtual ICollection<ClienteSoftwareUsers> ClienteSoftwareUsers { get; set; }
        public virtual ICollection<ClienteUsers> ClienteUsers { get; set; }
        public virtual ICollection<UsersPerfil> UsersPerfil { get; set; }
        public virtual ICollection<WidgetUsers> WidgetUsers { get; set; }
    }
}
