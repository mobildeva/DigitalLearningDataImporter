using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd
{
    public partial class Widget
    {
        public Widget()
        {
            WidgetModulos = new HashSet<WidgetModulos>();
            WidgetUsers = new HashSet<WidgetUsers>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string RutaUsersControl { get; set; }
        public string Css { get; set; }
        public bool? Activo { get; set; }
        public string NombreControl { get; set; }

        public virtual ICollection<WidgetModulos> WidgetModulos { get; set; }
        public virtual ICollection<WidgetUsers> WidgetUsers { get; set; }
    }
}
