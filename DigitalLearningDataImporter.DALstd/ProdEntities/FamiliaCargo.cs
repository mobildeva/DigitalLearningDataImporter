using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd.ProdEntities
{
    public partial class FamiliaCargo
    {
        public FamiliaCargo()
        {
            InformacionPersonal = new HashSet<InformacionPersonal>();
            Cargos = new HashSet<Cargos>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? IdSociedad { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<InformacionPersonal> InformacionPersonal { get; set; }
        public virtual ICollection<Cargos> Cargos { get; set; }
        public virtual Sociedad IdSociedadNavigation { get; set; }
    }
}
