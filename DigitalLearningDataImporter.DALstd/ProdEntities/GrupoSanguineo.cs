using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd.ProdEntities
{
    public partial class GrupoSanguineo
    {
        public GrupoSanguineo()
        {
            InformacionPersonal = new HashSet<InformacionPersonal>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }

        public virtual ICollection<InformacionPersonal> InformacionPersonal { get; set; }
    }
}
