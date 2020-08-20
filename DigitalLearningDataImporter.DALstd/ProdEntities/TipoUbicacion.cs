using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd.ProdEntities
{
    public partial class TipoUbicacion
    {
        public TipoUbicacion()
        {
            Ubicacion = new HashSet<Ubicacion>();
        }

        public byte Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }

        public virtual ICollection<Ubicacion> Ubicacion { get; set; }
    }
}
