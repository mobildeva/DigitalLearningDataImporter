using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd
{
    public partial class Clientes
    {
        public Clientes()
        {
            ClienteSoftware = new HashSet<ClienteSoftware>();
            ClienteUsers = new HashSet<ClienteUsers>();
            WidgetUsers = new HashSet<WidgetUsers>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdSociedad { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<ClienteSoftware> ClienteSoftware { get; set; }
        public virtual ICollection<ClienteUsers> ClienteUsers { get; set; }
        public virtual ICollection<WidgetUsers> WidgetUsers { get; set; }
    }
}
