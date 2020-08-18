using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd
{
    public partial class ClienteUsers
    {
        public int Id { get; set; }
        public int? IdUsers { get; set; }
        public int? IdClientes { get; set; }
        public bool? Activo { get; set; }

        public virtual Clientes IdClientesNavigation { get; set; }
        public virtual Users IdUsersNavigation { get; set; }
    }
}
