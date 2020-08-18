using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd
{
    public partial class ConfiguracionWidget
    {
        public int Id { get; set; }
        public int? IdWidget { get; set; }
        public int? IdGenerico { get; set; }
        public string Nombre { get; set; }
        public string Link { get; set; }
        public string Observacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaExpericion { get; set; }
        public bool? Activo { get; set; }

        public virtual WidgetUsers IdWidgetNavigation { get; set; }
    }
}
