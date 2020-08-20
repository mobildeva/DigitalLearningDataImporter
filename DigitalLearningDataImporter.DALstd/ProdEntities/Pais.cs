using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd.ProdEntities
{
    public partial class Pais
    {
        public int IdPais { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }
        public string CodigoArea { get; set; }
        public int CodigoSence { get; set; }
    }
}
