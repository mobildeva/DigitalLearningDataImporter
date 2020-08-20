using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd.ProdEntities
{
    public partial class ReglaPlanHorario
    {
        public ReglaPlanHorario()
        {
            InformacionPersonal = new HashSet<InformacionPersonal>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }
        public string UsuarioCr { get; set; }
        public DateTime FechaCr { get; set; }
        public string UsuarioUp { get; set; }
        public DateTime? FechaUp { get; set; }

        public virtual ICollection<InformacionPersonal> InformacionPersonal { get; set; }
    }
}
