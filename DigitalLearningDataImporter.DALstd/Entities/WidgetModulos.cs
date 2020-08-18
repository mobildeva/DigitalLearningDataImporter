using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd
{
    public partial class WidgetModulos
    {
        public int Id { get; set; }
        public int? IdWidget { get; set; }
        public int? IdModulos { get; set; }
        public bool? Activo { get; set; }

        public virtual Modulos IdModulosNavigation { get; set; }
        public virtual Widget IdWidgetNavigation { get; set; }
    }
}
