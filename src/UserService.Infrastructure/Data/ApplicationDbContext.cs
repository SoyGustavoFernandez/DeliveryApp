using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entity;

namespace UserService.Infrastructure.Data
{
    // <summary>
    /// Contexto de base de datos para la aplicación, que representa una sesión con la base de datos.
    /// </summary>
    /// <remarks>
    /// Esta clase coordina la funcionalidad de Entity Framework Core para las operaciones CRUD,
    /// administrando las entidades Usuario, Rol, Permiso y sus relaciones.
    /// </remarks>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Constructor del contexto de base de datos.
        /// </summary>
        /// <param name="options">Opciones de configuración para el contexto.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Conjunto de entidades para los Usuarios.
        /// </summary>
        public DbSet<Usuario> Usuarios { get; set; }

        /// <summary>
        /// Conjunto de entidades para los Roles.
        /// </summary>
        public DbSet<Rol> Roles { get; set; }

        /// <summary>
        /// Conjunto de entidades para los Permisos.
        /// </summary>
        public DbSet<Permiso> Permisos { get; set; }

        /// <summary>
        /// Conjunto de entidades para las relaciones Usuario-Rol.
        /// </summary>
        public DbSet<UsuarioRol> UsuarioRoles { get; set; }

        /// <summary>
        /// Conjunto de entidades para las relaciones Rol-Permiso.
        /// </summary>
        public DbSet<RolPermiso> RolPermisos { get; set; }

        /// <summary>
        /// Configura el modelo de datos y sus relaciones al crear el modelo.
        /// </summary>
        /// <param name="modelBuilder">Constructor para el modelo de datos.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id).ValueGeneratedOnAdd();

                entity.HasIndex(u => u.Email).IsUnique();

                entity.Property(u => u.PrimerNombre).IsRequired().HasMaxLength(50);
                entity.Property(u => u.SegundoNombre).IsRequired().HasMaxLength(50);
                entity.Property(u => u.ApellidoPaterno).IsRequired().HasMaxLength(50);
                entity.Property(u => u.ApellidoMaterno).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
                entity.Property(u => u.PasswordHash).IsRequired();
                entity.Property(u => u.PasswordSalt).IsRequired();
                entity.Property(u => u.Telefono).HasMaxLength(20);
                entity.Property(u => u.Direccion).HasMaxLength(1000);
                entity.Property(u => u.FechaRegistro).IsRequired().HasDefaultValueSql("GETUTCDATE()").ValueGeneratedOnAdd();
                entity.Property(u => u.FechaActualizacion).IsRequired().HasDefaultValueSql("GETUTCDATE()").ValueGeneratedOnAddOrUpdate();

            });

            // Configuración de Rol
            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Id).ValueGeneratedOnAdd();

                entity.HasIndex(r => r.Nombre).IsUnique();

                entity.Property(r => r.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(r => r.Descripcion).HasMaxLength(1000);
            });

            // Configuración de Permiso
            modelBuilder.Entity<Permiso>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();

                entity.HasIndex(p => p.Nombre).IsUnique();

                entity.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Descripcion).HasMaxLength(1000);
            });

            // Configuración de la relación muchos-a-muchos Usuario-Rol
            modelBuilder.Entity<UsuarioRol>(entity =>
            {
                entity.HasKey(ur => new { ur.UsuarioId, ur.RolId });

                entity.HasOne(ur => ur.Usuario)
                    .WithMany(u => u.UsuarioRoles)
                    .HasForeignKey(ur => ur.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ur => ur.Rol)
                    .WithMany(r => r.UsuarioRoles)
                    .HasForeignKey(ur => ur.RolId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de la relación muchos-a-muchos Rol-Permiso
            modelBuilder.Entity<RolPermiso>(entity =>
            {
                entity.HasKey(rp => new { rp.RolId, rp.PermisoId });

                entity.HasOne(rp => rp.Rol)
                    .WithMany(r => r.RolPermisos)
                    .HasForeignKey(rp => rp.RolId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(rp => rp.Permiso)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(rp => rp.PermisoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}