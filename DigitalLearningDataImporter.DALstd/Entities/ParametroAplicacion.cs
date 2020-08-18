using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd
{
    public partial class ParametroAplicacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdSociedad { get; set; }
        public string Valor { get; set; }
        public bool Activo { get; set; }
    }
}
