using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd.ProdEntities
{
    public partial class NivelOcupacional
    {
        public int Id { get; set; }
        public int? IdSociedad { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }
    }
}
