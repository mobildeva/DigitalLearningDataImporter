﻿using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd.ProdEntities
{
    public partial class UnidadesNegocio
    {
        public UnidadesNegocio()
        {
            PosicionLaboral = new HashSet<PosicionLaboral>();
            Locales = new HashSet<Locales>();
        }
        public int Id { get; set; }
        public int IdSociedad { get; set; }
        public string Nombre { get; set; }
        public int? IdUnidadOrganizacional { get; set; }
        public int? IdPersonaJefe { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdUbicacionFisica { get; set; }
        public string CodigoErp { get; set; }
        public bool? Activo { get; set; }

        public virtual Personas IdPersonaJefeNavigation { get; set; }
        public virtual UnidadesOrganizacional IdUnidadOrganizacionalNavigation { get; set; }
        public virtual Sociedad IdSociedadNavigation { get; set; }
        public virtual ICollection<PosicionLaboral> PosicionLaboral { get; set; }
        public virtual ICollection<Locales> Locales { get; set; }
    }
}
