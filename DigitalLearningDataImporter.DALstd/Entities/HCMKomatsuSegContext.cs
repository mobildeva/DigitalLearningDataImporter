using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DigitalLearningDataImporter.DALstd
{
    public partial class HCMKomatsuSegContext : DbContext
    {
        public HCMKomatsuSegContext()
        {
        }

        public HCMKomatsuSegContext(DbContextOptions<HCMKomatsuSegContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ClienteSoftware> ClienteSoftware { get; set; }
        public virtual DbSet<ClienteSoftwareUsers> ClienteSoftwareUsers { get; set; }
        public virtual DbSet<ClienteUsers> ClienteUsers { get; set; }
        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<ClientesSoftwareModulos> ClientesSoftwareModulos { get; set; }
        public virtual DbSet<ConfiguracionWidget> ConfiguracionWidget { get; set; }
        public virtual DbSet<Funcionalidades> Funcionalidades { get; set; }
        public virtual DbSet<LogUpdateUserPerfil> LogUpdateUserPerfil { get; set; }
        public virtual DbSet<MColaboradorVersionCapacitacion> MColaboradorVersionCapacitacion { get; set; }
        public virtual DbSet<ModuloPaginaFuncionalidad> ModuloPaginaFuncionalidad { get; set; }
        public virtual DbSet<Modulos> Modulos { get; set; }
        public virtual DbSet<Paginas> Paginas { get; set; }
        public virtual DbSet<ParametroAplicacion> ParametroAplicacion { get; set; }
        public virtual DbSet<Perfil> Perfil { get; set; }
        public virtual DbSet<Software> Software { get; set; }
        public virtual DbSet<SoftwareModulo> SoftwareModulo { get; set; }
        public virtual DbSet<TestClasses> TestClasses { get; set; }
        public virtual DbSet<Tests> Tests { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UsersPerfil> UsersPerfil { get; set; }
        public virtual DbSet<UsrDobles> UsrDobles { get; set; }
        public virtual DbSet<UsuarioPerfil> UsuarioPerfil { get; set; }
        public virtual DbSet<Widget> Widget { get; set; }
        public virtual DbSet<WidgetModulos> WidgetModulos { get; set; }
        public virtual DbSet<WidgetUsers> WidgetUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=HCMKomatsuSeg;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClienteSoftware>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK2")
                    .IsClustered(false);

                entity.Property(e => e.Activo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.HasOne(d => d.IdClientesNavigation)
                    .WithMany(p => p.ClienteSoftware)
                    .HasForeignKey(d => d.IdClientes)
                    .HasConstraintName("RefClientes1");

                entity.HasOne(d => d.IdSoftwareNavigation)
                    .WithMany(p => p.ClienteSoftware)
                    .HasForeignKey(d => d.IdSoftware)
                    .HasConstraintName("RefSoftware2");
            });

            modelBuilder.Entity<ClienteSoftwareUsers>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK10")
                    .IsClustered(false);

                entity.Property(e => e.Activo).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdUsersNavigation)
                    .WithMany(p => p.ClienteSoftwareUsers)
                    .HasForeignKey(d => d.IdUsers)
                    .HasConstraintName("RefUsers16");
            });

            modelBuilder.Entity<ClienteUsers>(entity =>
            {
                entity.HasOne(d => d.IdClientesNavigation)
                    .WithMany(p => p.ClienteUsers)
                    .HasForeignKey(d => d.IdClientes)
                    .HasConstraintName("FK_ClienteUsers_Clientes");

                entity.HasOne(d => d.IdUsersNavigation)
                    .WithMany(p => p.ClienteUsers)
                    .HasForeignKey(d => d.IdUsers)
                    .HasConstraintName("FK_ClienteUsers_Users");
            });

            modelBuilder.Entity<Clientes>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK1")
                    .IsClustered(false);

                entity.Property(e => e.Nombre).IsUnicode(false);
            });

            modelBuilder.Entity<ClientesSoftwareModulos>(entity =>
            {
                entity.Property(e => e.Activo).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdClienteSoftwareNavigation)
                    .WithMany(p => p.ClientesSoftwareModulos)
                    .HasForeignKey(d => d.IdClienteSoftware)
                    .HasConstraintName("FK_ClientesSoftwareModulos_ClienteSoftware");

                entity.HasOne(d => d.IdSoftwareModuloNavigation)
                    .WithMany(p => p.ClientesSoftwareModulos)
                    .HasForeignKey(d => d.IdSoftwareModulo)
                    .HasConstraintName("FK_ClientesSoftwareModulos_SoftwareModulo");
            });

            modelBuilder.Entity<ConfiguracionWidget>(entity =>
            {
                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaExpericion).HasColumnType("datetime");

                entity.Property(e => e.Link)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdWidgetNavigation)
                    .WithMany(p => p.ConfiguracionWidget)
                    .HasForeignKey(d => d.IdWidget)
                    .HasConstraintName("FK_ConfiguracionWidget_WidgetUsers");
            });

            modelBuilder.Entity<Funcionalidades>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK7")
                    .IsClustered(false);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Nombre).IsUnicode(false);

                entity.Property(e => e.Tag)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LogUpdateUserPerfil>(entity =>
            {
                entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");

                entity.Property(e => e.UsuarioSql)
                    .HasColumnName("UsuarioSQL")
                    .HasMaxLength(50)
                    .IsFixedLength();
            });

            modelBuilder.Entity<MColaboradorVersionCapacitacion>(entity =>
            {
                entity.ToTable("M_ColaboradorVersionCapacitacion");

                entity.Property(e => e.EstadoEvento)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IdUc).HasColumnName("IdUC");

                entity.Property(e => e.Resultado)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SituacionFinal)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ModuloPaginaFuncionalidad>(entity =>
            {
                entity.HasIndex(e => e.IdPaginas)
                    .HasName("idx_ModuloPaginaFuncionalidad_IdPaginas");

                entity.HasIndex(e => e.IdPerfil)
                    .HasName("idx_ModuloPaginaFuncionalidad_IdPerfil");

                entity.Property(e => e.Estado).HasDefaultValueSql("((0))");

                entity.Property(e => e.Nombre).IsUnicode(false);

                entity.HasOne(d => d.IdFuncionalidadesNavigation)
                    .WithMany(p => p.ModuloPaginaFuncionalidad)
                    .HasForeignKey(d => d.IdFuncionalidades)
                    .HasConstraintName("RefFuncionalidades7");

                entity.HasOne(d => d.IdPaginasNavigation)
                    .WithMany(p => p.ModuloPaginaFuncionalidad)
                    .HasForeignKey(d => d.IdPaginas)
                    .HasConstraintName("RefPaginas6");

                entity.HasOne(d => d.IdPerfilNavigation)
                    .WithMany(p => p.ModuloPaginaFuncionalidad)
                    .HasForeignKey(d => d.IdPerfil)
                    .HasConstraintName("FK_ModuloPaginaFuncionalidad_Perfil");
            });

            modelBuilder.Entity<Modulos>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK5")
                    .IsClustered(false);

                entity.Property(e => e.Activo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Nombre).IsUnicode(false);
            });

            modelBuilder.Entity<Paginas>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK6")
                    .IsClustered(false);

                entity.HasIndex(e => new { e.IdPadre, e.Orden, e.Orientacion })
                    .HasName("Filtros");

                entity.Property(e => e.Activo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre).IsUnicode(false);

                entity.Property(e => e.Orientacion).HasDefaultValueSql("((1))");

                entity.Property(e => e.Pagina).IsUnicode(false);

                entity.HasOne(d => d.IdPadreNavigation)
                    .WithMany(p => p.InverseIdPadreNavigation)
                    .HasForeignKey(d => d.IdPadre)
                    .HasConstraintName("FK_Paginas_Paginas");

                entity.HasOne(d => d.IdSoftwareModuloNavigation)
                    .WithMany(p => p.Paginas)
                    .HasForeignKey(d => d.IdSoftwareModulo)
                    .HasConstraintName("FK_Paginas_SoftwareModulo");
            });

            modelBuilder.Entity<ParametroAplicacion>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Valor)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Perfil>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK11")
                    .IsClustered(false);

                entity.Property(e => e.Activo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.HasOne(d => d.IdPaginaInicioNavigation)
                    .WithMany(p => p.Perfil)
                    .HasForeignKey(d => d.IdPaginaInicio)
                    .HasConstraintName("FK_Perfil_PaginasI");
            });

            modelBuilder.Entity<Software>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK3")
                    .IsClustered(false);

                entity.Property(e => e.Activo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre).IsUnicode(false);
            });

            modelBuilder.Entity<SoftwareModulo>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK4")
                    .IsClustered(false);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.HasOne(d => d.IdModulosNavigation)
                    .WithMany(p => p.SoftwareModulo)
                    .HasForeignKey(d => d.IdModulos)
                    .HasConstraintName("RefModulos22");

                entity.HasOne(d => d.IdSoftwareNavigation)
                    .WithMany(p => p.SoftwareModulo)
                    .HasForeignKey(d => d.IdSoftware)
                    .HasConstraintName("RefSoftware21");
            });

            modelBuilder.Entity<TestClasses>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("TestClasses", "tSQLt");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<Tests>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Tests", "tSQLt");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.TestClassName)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK9")
                    .IsClustered(false);

                entity.HasIndex(e => e.Username)
                    .HasName("idxUsers_Username");

                entity.Property(e => e.Activo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Bloqueado).HasDefaultValueSql("((0))");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.FechaToken).HasColumnType("datetime");

                entity.Property(e => e.FechaUltimoIntento).HasColumnType("datetime");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroIntentosFallidos).HasDefaultValueSql("((0))");

                entity.Property(e => e.Password).HasMaxLength(550);

                entity.Property(e => e.Token)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UsersPerfil>(entity =>
            {
                entity.HasIndex(e => new { e.IdPerfil, e.IdUsers })
                    .HasName("idxUsersPerfil_idperfil_idusers");

                entity.HasIndex(e => new { e.IdUsers, e.IdPerfil })
                    .HasName("idxUsersPerfil_idperfil");

                entity.HasOne(d => d.IdPerfilNavigation)
                    .WithMany(p => p.UsersPerfil)
                    .HasForeignKey(d => d.IdPerfil)
                    .HasConstraintName("FK_UsersPerfil_Perfil");

                entity.HasOne(d => d.IdUsersNavigation)
                    .WithMany(p => p.UsersPerfil)
                    .HasForeignKey(d => d.IdUsers)
                    .HasConstraintName("FK_UsersPerfil_Users");
            });

            modelBuilder.Entity<UsrDobles>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("usr_dobles");

                entity.Property(e => e.Cuenta).HasColumnName("cuenta");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UsuarioPerfil>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK12")
                    .IsClustered(false);

                entity.Property(e => e.Activo).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdClienteSoftwareUsersNavigation)
                    .WithMany(p => p.UsuarioPerfil)
                    .HasForeignKey(d => d.IdClienteSoftwareUsers)
                    .HasConstraintName("RefCliente/Software/Users19");

                entity.HasOne(d => d.IdPerfilNavigation)
                    .WithMany(p => p.UsuarioPerfil)
                    .HasForeignKey(d => d.IdPerfil)
                    .HasConstraintName("RefPerfil20");
            });

            modelBuilder.Entity<Widget>(entity =>
            {
                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.NombreControl)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.RutaUsersControl).HasMaxLength(100);
            });

            modelBuilder.Entity<WidgetModulos>(entity =>
            {
                entity.HasOne(d => d.IdModulosNavigation)
                    .WithMany(p => p.WidgetModulos)
                    .HasForeignKey(d => d.IdModulos)
                    .HasConstraintName("FK_WidgetModulos_Modulos");

                entity.HasOne(d => d.IdWidgetNavigation)
                    .WithMany(p => p.WidgetModulos)
                    .HasForeignKey(d => d.IdWidget)
                    .HasConstraintName("FK_WidgetModulos_Widget");
            });

            modelBuilder.Entity<WidgetUsers>(entity =>
            {
                entity.HasOne(d => d.IdClientesNavigation)
                    .WithMany(p => p.WidgetUsers)
                    .HasForeignKey(d => d.IdClientes)
                    .HasConstraintName("FK_WidgetUsers_Clientes");

                entity.HasOne(d => d.IdUsersNavigation)
                    .WithMany(p => p.WidgetUsers)
                    .HasForeignKey(d => d.IdUsers)
                    .HasConstraintName("FK_WidgetUsers_Users");

                entity.HasOne(d => d.IdWidgetNavigation)
                    .WithMany(p => p.WidgetUsers)
                    .HasForeignKey(d => d.IdWidget)
                    .HasConstraintName("FK_WidgetUsers_Widget");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
