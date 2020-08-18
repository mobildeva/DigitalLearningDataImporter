using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd.ProdEntities
{
    public partial class Ubicacion
    {
        public Ubicacion()
        {
            InformacionPersonal = new HashSet<InformacionPersonal>();
            InverseIdPadreNavigation = new HashSet<Ubicacion>();
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
        public virtual ICollection<InformacionPersonal> InformacionPersonal { get; set; }
        public virtual ICollection<Ubicacion> InverseIdPadreNavigation { get; set; }
        public virtual ICollection<PosicionLaboral> PosicionLaboral { get; set; }
    }
}
