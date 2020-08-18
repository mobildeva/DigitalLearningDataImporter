using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd
{
    public partial class LogUpdateUserPerfil
    {
        public int Id { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string UsuarioSql { get; set; }
        public int? IdPersona { get; set; }
        public int? IdPerfilOld { get; set; }
        public int? IdPerfilNew { get; set; }
    }
}
