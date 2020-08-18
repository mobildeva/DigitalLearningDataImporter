using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd.ProdEntities
{
    public partial class TipoContrato
    {
        public TipoContrato()
        {
            PosicionLaboral = new HashSet<PosicionLaboral>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<PosicionLaboral> PosicionLaboral { get; set; }
    }
}
