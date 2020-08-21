using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DigitalLearningDataImporter.DALstd.ProdEntities
{
    public partial class HCMKomatsuProdContext : DbContext
    {
        public HCMKomatsuProdContext()
        {
        }

        public HCMKomatsuProdContext(DbContextOptions<HCMKomatsuProdContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EscolaridadSence> EscolaridadSence { get; set; }
        public virtual DbSet<FamiliaCargo> FamiliaCargo { get; set; }
        public virtual DbSet<Genero> Genero { get; set; }
        public virtual DbSet<GrupoSanguineo> GrupoSanguineo { get; set; }
        public virtual DbSet<InformacionPersonal> InformacionPersonal { get; set; }
        public virtual DbSet<PosicionLaboral> PosicionLaboral { get; set; }
        public virtual DbSet<TipoContrato> TipoContrato { get; set; }
        public virtual DbSet<UnidadesNegocio> UnidadesNegocio { get; set; }
        public virtual DbSet<Afp> Afp { get; set; }
        public virtual DbSet<CentroCosto> CentroCosto { get; set; }
        public virtual DbSet<Sociedad> Sociedad { get; set; }
        public virtual DbSet<TipoUbicacion> TipoUbicacion { get; set; }
        public virtual DbSet<Ubicacion> Ubicacion { get; set; }
        public virtual DbSet<UnidadesOrganizacional> UnidadesOrganizacional { get; set; }
        public virtual DbSet<EstadoCivil> EstadoCivil { get; set; }
        public virtual DbSet<Pais> Pais { get; set; }
        public virtual DbSet<Personas> Personas { get; set; }
        public virtual DbSet<Cargos> Cargos { get; set; }
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Locales> Locales { get; set; }
        public virtual DbSet<NivelOcupacional> NivelOcupacional { get; set; }
        public virtual DbSet<Isapres> Isapres { get; set; }
        public virtual DbSet<JornadaLaboral> JornadaLaboral { get; set; }
        public virtual DbSet<ReglaPlanHorario> ReglaPlanHorario { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=HCMKomatsuProd;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CentroCosto>(entity =>
            {
                entity.HasIndex(e => e.Nombre)
                    .HasName("UQ_CC_NameCentroCosto")
                    .IsUnique();

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSociedadNavigation)
                    .WithMany(p => p.CentroCosto)
                    .HasForeignKey(d => d.IdSociedad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CentroCosto_Sociedad");
            });

            modelBuilder.Entity<EscolaridadSence>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FamiliaCargo>(entity =>
            {
                entity.HasIndex(e => e.IdSociedad)
                    .HasName("IX_FK_FamiliaCargo_Sociedad");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSociedadNavigation)
                    .WithMany(p => p.FamiliaCargo)
                    .HasForeignKey(d => d.IdSociedad)
                    .HasConstraintName("FK_FamiliaCargo_Sociedad");
            });

            modelBuilder.Entity<Genero>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GrupoSanguineo>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength();
            });

            modelBuilder.Entity<InformacionPersonal>(entity =>
            {
                entity.Property(e => e.AutorizoNotificacionPersonal).HasDefaultValueSql("((0))");

                entity.Property(e => e.ClaseLicenciaConducir)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmailPersonal)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaMatrimonio).HasColumnType("date");

                entity.Property(e => e.FechaMod)
                    .HasColumnType("datetime")
                    .HasComment("Fecha en que se realizó un cambio en los datos de la persona.");

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.FechaVencLicenciaConducir).HasColumnType("date");

                entity.Property(e => e.FonoFijoEmergencia)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MovilEmergencia)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.NombreContactoEmergencia)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Numero)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroLicenciaConducir)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroPasaporte)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroSeguridadSocial)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Otro)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SituacionMilitar)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TallaCamisa)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.TallaPantalon)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.TallaZapatos)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.TelefonoFijo)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.TelefonoMovil)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioMod).HasComment("Usuario que realizó una modificación en los datos de la persona");

                entity.HasOne(d => d.IdFamiliaCargoNavigation)
                    .WithMany(p => p.InformacionPersonal)
                    .HasForeignKey(d => d.IdFamiliaCargo)
                    .HasConstraintName("FK_InformacionPersonal_FamiliaCargo");

                entity.HasOne(d => d.IdGeneroNavigation)
                    .WithMany(p => p.InformacionPersonal)
                    .HasForeignKey(d => d.IdGenero)
                    .HasConstraintName("FK_InformacionPersonal_Genero");

                entity.HasOne(d => d.IdGrupoSanguineoNavigation)
                    .WithMany(p => p.InformacionPersonal)
                    .HasForeignKey(d => d.IdGrupoSanguineo)
                    .HasConstraintName("FK_InformacionPersonal_GrupoSanguineo");

                entity.HasOne(d => d.IdUbicacionNavigation)
                    .WithMany(p => p.InformacionPersonal)
                    .HasForeignKey(d => d.IdUbicacion)
                    .HasConstraintName("FK_InformacionPersonal_Ubicacion");

                entity.HasOne(d => d.IdReglaPlanHorarioNavigation)
                   .WithMany(p => p.InformacionPersonal)
                   .HasForeignKey(d => d.IdReglaPlanHorario)
                   .HasConstraintName("FK_InformacionPersonal_ReglaPlanHorario");
            });

            modelBuilder.Entity<PosicionLaboral>(entity =>
            {
                entity.HasIndex(e => e.IdCargo)
                    .HasName("IX_FK_HistoriaLaboral_Cargos");

                entity.HasIndex(e => e.IdPersona)
                    .HasName("IX_FK_HistoriaLaboral_Personas");

                entity.HasIndex(e => e.IdSociedad)
                    .HasName("IX_FK_HistoriaLaboral_Sociedad");

                entity.HasIndex(e => e.IdTipoPosicion)
                    .HasName("IX_FK_HistoriaLaboral_TipoPosicion");

                entity.HasIndex(e => e.IdUbicacion)
                    .HasName("IX_FK_HistoriaLaboral_Comuna");

                entity.HasIndex(e => e.IdUnidadNegocio)
                    .HasName("IX_FK_HistoriaLaboral_UnidadesNegocio");

                entity.HasIndex(e => e.IdUnidadOrganizacional)
                    .HasName("IX_FK_HistoriaLaboral_UnidadesOrganizacional");

                entity.HasIndex(e => new { e.IdUnidadOrganizacional, e.IdUnidadNegocio, e.IdCargo, e.IdSociedad, e.Estado, e.Activo })
                    .HasName("IDX_PosicionLaboral_IdUnidadOrganizacional_IdUnidadNegocio_IdCargo_IdSociedad_Estado_Activo");

                entity.Property(e => e.ComentarioDesvinculacion)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FechaInicioContrato).HasColumnType("datetime");

                entity.Property(e => e.FechaInicioPosicion).HasColumnType("datetime");

                entity.Property(e => e.FechaTerminoContrato).HasColumnType("datetime");

                entity.Property(e => e.FechaTerminoPosicion).HasColumnType("datetime");

                entity.Property(e => e.FranquiciaSence)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NombrePosicion).IsUnicode(false);

                entity.Property(e => e.NombrePosicionAnterior).IsUnicode(false);

                entity.Property(e => e.SociedadAnterior)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.PosicionLaboralIdPersonaNavigation)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HistoriaLaboral_Personas");

                entity.HasOne(d => d.IdPersonaCambioNavigation)
                    .WithMany(p => p.PosicionLaboralIdPersonaCambioNavigation)
                    .HasForeignKey(d => d.IdPersonaCambio)
                    .HasConstraintName("FK_PosicionLaboral_Personas1");

                entity.HasOne(d => d.IdPersonaJefeNavigation)
                    .WithMany(p => p.PosicionLaboralIdPersonaJefeNavigation)
                    .HasForeignKey(d => d.IdPersonaJefe)
                    .HasConstraintName("FK_PosicionLaboral_Personas");

                entity.HasOne(d => d.IdUnidadNegocioNavigation)
                    .WithMany(p => p.PosicionLaboral)
                    .HasForeignKey(d => d.IdUnidadNegocio)
                    .HasConstraintName("FK_HistoriaLaboral_UnidadesNegocio");

                entity.HasOne(d => d.IdEscolaridadSenceNavigation)
                    .WithMany(p => p.PosicionLaboral)
                    .HasForeignKey(d => d.IdEscolaridadSence)
                    .HasConstraintName("FK_PosicionLaboral_EscolaridadSence");

                entity.HasOne(d => d.IdTipoContratoNavigation)
                   .WithMany(p => p.PosicionLaboral)
                   .HasForeignKey(d => d.IdTipoContrato)
                   .HasConstraintName("FK_PosicionLaboral_TipoContrato");

                entity.HasOne(d => d.IdUbicacionNavigation)
                    .WithMany(p => p.PosicionLaboral)
                    .HasForeignKey(d => d.IdUbicacion)
                    .HasConstraintName("FK_PosicionLaboral_Ubicacion");
            });

            modelBuilder.Entity<TipoContrato>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ubicacion>(entity =>
            {
                entity.Property(e => e.CodigoArea)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPadreNavigation)
                    .WithMany(p => p.InverseIdPadreNavigation)
                    .HasForeignKey(d => d.IdPadre)
                    .HasConstraintName("FK_Ubicacion_Ubicacion");
            });

            modelBuilder.Entity<Afp>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Sociedad>(entity =>
            {
                entity.HasIndex(e => e.SiglaSociedad)
                    .HasName("sigla");

                entity.HasIndex(e => new { e.Id, e.Activo })
                    .HasName("IDX_Sociedad_Id_Activo");

                entity.Property(e => e.ClaveSence)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CodErp)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CorreoContacto)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Fax)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fono)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdentificacionUnica)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Logo)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.NombreContacto)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Portal)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SiglaSociedad)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Skin)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdUbicacionNavigation)
                    .WithMany(p => p.Sociedad)
                    .HasForeignKey(d => d.IdUbicacion)
                    .HasConstraintName("FK_Sociedad_Ubicacion");
            });

            modelBuilder.Entity<UnidadesNegocio>(entity =>
            {
                entity.HasIndex(e => e.IdCentroCosto)
                    .HasName("IX_FK_UnidadesNegocio_CentroCosto");

                entity.HasIndex(e => e.IdSociedad)
                    .HasName("IX_FK_UnidadesNegocio_Sociedad");

                entity.HasIndex(e => e.IdUbicacionFisica)
                    .HasName("IX_FK_UNIDADES_NEGOCIO_UBICACIONES_FISICAS");

                entity.HasIndex(e => e.Nombre)
                    .HasName("UQ_CC_NameUN")
                    .IsUnique();

                entity.HasIndex(e => new { e.Id, e.IdUnidadOrganizacional, e.IdSociedad, e.Activo })
                    .HasName("IDX_UnidadNegocio_Id_IdUnidadOrganizacional_IdSociedad_Activo");

                entity.Property(e => e.CodigoErp)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSociedadNavigation)
                    .WithMany(p => p.UnidadesNegocio)
                    .HasForeignKey(d => d.IdSociedad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UnidadesNegocio_Sociedad");

                entity.HasOne(d => d.IdPersonaJefeNavigation)
                  .WithMany(p => p.UnidadesNegocio)
                  .HasForeignKey(d => d.IdPersonaJefe)
                  .HasConstraintName("FK_UnidadesNegocio_Personas");

                entity.HasOne(d => d.IdUnidadOrganizacionalNavigation)
                    .WithMany(p => p.UnidadesNegocio)
                    .HasForeignKey(d => d.IdUnidadOrganizacional)
                    .HasConstraintName("FK_UnidadesNegocio_UnidadesOrganizacional");
            });

            modelBuilder.Entity<UnidadesOrganizacional>(entity =>
            {
                entity.HasIndex(e => e.IdCentroCosto)
                    .HasName("IX_FK_UnidadesOrganizacional_CentroCosto");

                entity.HasIndex(e => e.IdSociedad)
                    .HasName("IX_FK_UnidadesOrganizacional_Sociedad");

                entity.HasIndex(e => e.IdUbicacionesFisica)
                    .HasName("IX_FK_UNIDADES_ORGANIZACIONALES_UBICACIONES_FISICAS");

                entity.HasIndex(e => e.Nombre)
                    .HasName("UQ_CC_NameUnidadOrganizacional")
                    .IsUnique();

                entity.HasIndex(e => new { e.IdPadre, e.Id, e.IdSociedad, e.Activo })
                    .HasName("IDX_UnidadOrganizacional_IdPadre_Id_IdSociedad_Activo");

                entity.Property(e => e.CodigoErp)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPadreNavigation)
                    .WithMany(p => p.InverseIdPadreNavigation)
                    .HasForeignKey(d => d.IdPadre)
                    .HasConstraintName("FK_UnidadesOrganizacional_UnidadesOrganizacional");

                entity.HasOne(d => d.IdSociedadNavigation)
                    .WithMany(p => p.UnidadesOrganizacional)
                    .HasForeignKey(d => d.IdSociedad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UnidadesOrganizacional_Sociedad");

                entity.HasOne(d => d.IdCentroCostoNavigation)
                    .WithMany(p => p.UnidadesOrganizacional)
                    .HasForeignKey(d => d.IdCentroCosto)
                    .HasConstraintName("FK_UnidadesOrganizacional_CentroCosto");
            });

            modelBuilder.Entity<TipoUbicacion>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ubicacion>(entity =>
            {
                entity.Property(e => e.CodigoArea)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPadreNavigation)
                    .WithMany(p => p.InverseIdPadreNavigation)
                    .HasForeignKey(d => d.IdPadre)
                    .HasConstraintName("FK_Ubicacion_Ubicacion");

                entity.HasOne(d => d.IdTipoUbicacionNavigation)
                    .WithMany(p => p.Ubicacion)
                    .HasForeignKey(d => d.IdTipoUbicacion)
                    .HasConstraintName("FK_Ubicacion_TipoUbicacion");
            });

            modelBuilder.Entity<EstadoCivil>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pais>(entity =>
            {
                entity.HasKey(e => e.IdPais);

                entity.Property(e => e.CodigoArea)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Personas>(entity =>
            {
                entity.HasIndex(e => e.IdentificacionUnica)
                    .HasName("Rut");

                entity.Property(e => e.ApellidoMaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApellidoPaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Celular)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClaveSence)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ConectaSence).HasColumnName("ConectaSENCE");

                entity.Property(e => e.Dv)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Fono)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdentificacionUnica)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Cargos>(entity =>
            {
                entity.HasIndex(e => e.IdEscalaSalarial)
                    .HasName("IX_FK_Cargos_EscalaSalarial");

                entity.HasIndex(e => e.IdJornadaLaboral)
                    .HasName("IX_FK_Cargos_JornadaLaboral");

                entity.HasIndex(e => e.IdSociedad)
                    .HasName("IX_FK_Cargos_Sociedad");

                entity.HasIndex(e => e.IdUbicacionesFisicas)
                    .HasName("IX_FK_Cargos_UbicacionesFisica");

                entity.HasIndex(e => e.IdUnidadOrganizacional)
                    .HasName("IX_FK_Cargos_UnidadesOrganizacional");

                entity.HasIndex(e => e.Nombre)
                    .HasName("UQ_CC_NameCargos")
                    .IsUnique();

                entity.HasIndex(e => new { e.Id, e.IdUnidadOrganizacional, e.IdSociedad, e.Activo })
                    .HasName("IDX_Cargos_Id_IdUnidadOrganizacional_IdSociedad_Activo");

                entity.Property(e => e.Anexo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CodigoErp)
                    .HasColumnName("Codigo_Erp")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Usuariocreacion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Usuariomodificacion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdFamiliaCargoNavigation)
                    .WithMany(p => p.Cargos)
                    .HasForeignKey(d => d.IdFamiliaCargo)
                    .HasConstraintName("FK_Cargos_FamiliaCargo");

                entity.HasOne(d => d.IdSociedadNavigation)
                    .WithMany(p => p.Cargos)
                    .HasForeignKey(d => d.IdSociedad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cargos_Sociedad");

                entity.HasOne(d => d.IdUnidadOrganizacionalNavigation)
                    .WithMany(p => p.Cargos)
                    .HasForeignKey(d => d.IdUnidadOrganizacional)
                    .HasConstraintName("FK_Cargos_UnidadesOrganizacional");
            });

            modelBuilder.Entity<Area>(entity =>
            {
                entity.Property(e => e.FechaCr)
                    .HasColumnName("FechaCR")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaUp).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioCr)
                    .IsRequired()
                    .HasColumnName("UsuarioCR")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioUp)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Locales>(entity =>
            {
                entity.HasIndex(e => e.CodigoLocal)
                    .HasName("unq_CodigoLocal")
                    .IsUnique();

                entity.HasIndex(e => e.NombreLocal)
                    .HasName("unq_NombreLocal")
                    .IsUnique();

                entity.Property(e => e.CodigoLocal)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NombreLocal)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdFormatoNavigation)
                    .WithMany(p => p.Locales)
                    .HasForeignKey(d => d.IdFormato)
                    .HasConstraintName("FK_Locales_UnidadesNegocio").IsRequired(false);
            });

            modelBuilder.Entity<NivelOcupacional>(entity =>
            {
                entity.Property(e => e.Nombre).HasMaxLength(250);
            });

            modelBuilder.Entity<Isapres>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<JornadaLaboral>(entity =>
            {
                entity.Property(e => e.Descripcion)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ReglaPlanHorario>(entity =>
            {
                entity.Property(e => e.FechaCr)
                    .HasColumnName("FechaCR")
                    .HasColumnType("datetime");

                entity.Property(e => e.FechaUp).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioCr)
                    .IsRequired()
                    .HasColumnName("UsuarioCR")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioUp)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
