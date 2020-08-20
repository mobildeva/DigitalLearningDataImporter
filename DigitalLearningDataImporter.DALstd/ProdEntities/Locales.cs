using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd.ProdEntities
{
    public partial class Locales
    {
        public int Id { get; set; }
        public string CodigoLocal { get; set; }
        public string NombreLocal { get; set; }
        public bool Activo { get; set; }
        public int? IdFormato { get; set; }

        public virtual UnidadesNegocio IdFormatoNavigation { get; set; }
    }
}
