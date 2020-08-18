using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd.ProdEntities
{
    public partial class FamiliaCargo
    {
        public FamiliaCargo()
        {
            InformacionPersonal = new HashSet<InformacionPersonal>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? IdSociedad { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<InformacionPersonal> InformacionPersonal { get; set; }
    }
}
