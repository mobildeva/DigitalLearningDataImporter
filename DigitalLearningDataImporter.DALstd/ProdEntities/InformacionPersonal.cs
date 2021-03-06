﻿using System;
using System.Collections.Generic;

namespace DigitalLearningDataImporter.DALstd.ProdEntities
{
    public partial class InformacionPersonal
    {
        public int Id { get; set; }
        public int IdPersona { get; set; }
        public int? IdGenero { get; set; }
        public int? IdEstadoCivil { get; set; }
        public int? IdUbicacion { get; set; }
        public int? IdPaisNacionalidad { get; set; }
        public int? IdPaisResidencia { get; set; }
        public int? IdGrupoEtnico { get; set; }
        public int? IdGrupoSanguineo { get; set; }
        public int? IdIsapre { get; set; }
        public int? IdAfp { get; set; }
        public DateTime? FechaMatrimonio { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Fotografia { get; set; }
        public string TelefonoMovil { get; set; }
        public int? IdCodigoAreaPersFijo { get; set; }
        public string TelefonoFijo { get; set; }
        public string NombreContactoEmergencia { get; set; }
        public int? IdCodigoAreaPersEmerg { get; set; }
        public string FonoFijoEmergencia { get; set; }
        public string MovilEmergencia { get; set; }
        public int? Altura { get; set; }
        public int? Peso { get; set; }
        public string TallaPantalon { get; set; }
        public string TallaCamisa { get; set; }
        public string TallaZapatos { get; set; }
        public string EmailPersonal { get; set; }
        public string NumeroSeguridadSocial { get; set; }
        public bool? Fumador { get; set; }
        public string NumeroLicenciaConducir { get; set; }
        public string ClaseLicenciaConducir { get; set; }
        public DateTime? FechaVencLicenciaConducir { get; set; }
        public string SituacionMilitar { get; set; }
        public string NumeroPasaporte { get; set; }
        public bool? MovilidadGeografica { get; set; }
        public int? IdTipoDireccion { get; set; }
        public string Direccion { get; set; }
        public string Numero { get; set; }
        public string Otro { get; set; }
        public bool? Activo { get; set; }
        public string CurriculumVitae { get; set; }
        public bool? AutorizoNotificacionPersonal { get; set; }
        public int? IdFamiliaCargo { get; set; }
        public int? IdArea { get; set; }
        public int? IdReglaPlanHorario { get; set; }
        public int? JornadaLaboral { get; set; }
        public bool? CuentaReparto { get; set; }
        public bool? Sindizalizado { get; set; }
        public bool? Pensionado { get; set; }
        public bool? Discapacitado { get; set; }
        public int? IdLocal { get; set; }
        //public int? UsuarioMod { get; set; }
        //public DateTime? FechaMod { get; set; }

        public virtual FamiliaCargo IdFamiliaCargoNavigation { get; set; }
        public virtual Genero IdGeneroNavigation { get; set; }
        public virtual GrupoSanguineo IdGrupoSanguineoNavigation { get; set; }
        public virtual Ubicacion IdUbicacionNavigation { get; set; }
        public virtual ReglaPlanHorario IdReglaPlanHorarioNavigation { get; set; }
    }
}
