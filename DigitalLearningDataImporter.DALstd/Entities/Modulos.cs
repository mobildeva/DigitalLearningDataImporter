using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd
{
    public partial class Modulos
    {
        public Modulos()
        {
            SoftwareModulo = new HashSet<SoftwareModulo>();
            WidgetModulos = new HashSet<WidgetModulos>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime? Fecha { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<SoftwareModulo> SoftwareModulo { get; set; }
        public virtual ICollection<WidgetModulos> WidgetModulos { get; set; }
    }
}
