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
        public virtual DbSet<Ubicacion> Ubicacion { get; set; }
        public virtual DbSet<UnidadesNegocio> UnidadesNegocio { get; set; }
        public virtual DbSet<Afp> Afp { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=HCMKomatsuProd;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EscolaridadSence>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FamiliaCargo>(entity =>
            {
                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
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
            });

            modelBuilder.Entity<PosicionLaboral>(entity =>
            {
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
