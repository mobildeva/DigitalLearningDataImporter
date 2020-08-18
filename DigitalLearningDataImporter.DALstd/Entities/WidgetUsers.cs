using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd
{
    public partial class WidgetUsers
    {
        public WidgetUsers()
        {
            ConfiguracionWidget = new HashSet<ConfiguracionWidget>();
        }

        public int Id { get; set; }
        public int? IdUsers { get; set; }
        public int? IdClientes { get; set; }
        public int? IdWidget { get; set; }
        public int? Columna { get; set; }
        public int? Fila { get; set; }
        public bool? Activo { get; set; }

        public virtual Clientes IdClientesNavigation { get; set; }
        public virtual Users IdUsersNavigation { get; set; }
        public virtual Widget IdWidgetNavigation { get; set; }
        public virtual ICollection<ConfiguracionWidget> ConfiguracionWidget { get; set; }
    }
}
