using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd.ProdEntities
{
    public partial class Ubicacion
    {
        public Ubicacion()
        {
            InverseIdPadreNavigation = new HashSet<Ubicacion>();
            Sociedad = new HashSet<Sociedad>();
            InformacionPersonal = new HashSet<InformacionPersonal>();
            PosicionLaboral = new HashSet<PosicionLaboral>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public short Orden { get; set; }
        public int? IdPadre { get; set; }
        public byte? IdTipoUbicacion { get; set; }
        public string CodigoArea { get; set; }
        public bool? Activo { get; set; }
        public int CodigoUbicacionSence { get; set; }

        public virtual Ubicacion IdPadreNavigation { get; set; }
        public virtual TipoUbicacion IdTipoUbicacionNavigation { get; set; }
        public virtual ICollection<Ubicacion> InverseIdPadreNavigation { get; set; }
        public virtual ICollection<Sociedad> Sociedad { get; set; }
        public virtual ICollection<InformacionPersonal> InformacionPersonal { get; set; }
        public virtual ICollection<PosicionLaboral> PosicionLaboral { get; set; }
    }
}
