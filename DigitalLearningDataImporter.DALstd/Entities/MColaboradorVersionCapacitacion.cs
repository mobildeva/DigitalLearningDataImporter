using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd
{
    public partial class MColaboradorVersionCapacitacion
    {
        public int IdMalla { get; set; }
        public int IdVersionMalla { get; set; }
        public int IdEstadoVersion { get; set; }
        public int IdVersionUnidadCurricular { get; set; }
        public int IdUc { get; set; }
        public int IdVersionesColaborador { get; set; }
        public int IdColaborador { get; set; }
        public int? IdEvento { get; set; }
        public int? IdNominaEventos { get; set; }
        public string SituacionFinal { get; set; }
        public string EstadoEvento { get; set; }
        public int? IdModalidad { get; set; }
        public int? CantContenido { get; set; }
        public string Resultado { get; set; }
        public bool? Temp { get; set; }
        public int Id { get; set; }
        public int? IdEstadoEvento { get; set; }
    }
}
