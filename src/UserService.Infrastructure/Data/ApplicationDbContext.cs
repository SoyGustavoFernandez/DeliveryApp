using Microsoft.EntityFrameworkCore;
using RoleService.Domain.Entity;

namespace RoleService.Infrastructure.Data
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

                entity.HasIndex(u => u.Email).IsUnique();

                entity.Property(u => u.Id).ValueGeneratedOnAdd();
                entity.Property(u => u.PrimerNombre).IsRequired().HasMaxLength(50);
                entity.Property(u => u.SegundoNombre).IsRequired().HasMaxLength(50);
                entity.Property(u => u.ApellidoPaterno).IsRequired().HasMaxLength(50);
                entity.Property(u => u.ApellidoMaterno).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
                entity.Property(u => u.PasswordHash).IsRequired();
                entity.Property(u => u.PasswordSalt).IsRequired();
                entity.Property(u => u.Telefono).HasMaxLength(20);
                entity.Property(u => u.Direccion).HasMaxLength(1000);
                entity.Property(u => u.IdUsuReg).IsRequired().HasDefaultValue(Guid.Parse("4c86ff9d-fed2-43aa-ab6d-457525de1a88"));
                entity.Property(u => u.FecReg).IsRequired().HasDefaultValueSql("GETUTCDATE()").ValueGeneratedOnAdd();
                entity.Property(u => u.FecMod).HasDefaultValueSql("GETUTCDATE()").ValueGeneratedOnUpdate();
                entity.Property(u => u.Vigente).IsRequired().HasDefaultValue(true);
            });

            // Configuración de Rol
            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(r => r.Id);

                entity.Property(r => r.Id).ValueGeneratedOnAdd();
                entity.Property(r => r.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(r => r.Descripcion).HasMaxLength(1000);

                entity.Property(r => r.IdUsuReg).IsRequired().HasDefaultValue(Guid.Parse("4c86ff9d-fed2-43aa-ab6d-457525de1a88"));
                entity.Property(r => r.FecReg).IsRequired().HasDefaultValueSql("GETUTCDATE()").ValueGeneratedOnAdd();
                entity.Property(r => r.FecMod).HasDefaultValueSql("GETUTCDATE()").ValueGeneratedOnUpdate();
                entity.Property(r => r.Vigente).IsRequired().HasDefaultValue(true);
            });

            // Configuración de Permiso
            modelBuilder.Entity<Permiso>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Descripcion).HasMaxLength(1000);
                entity.Property(u => u.IdUsuReg).IsRequired().HasDefaultValue(Guid.Parse("4c86ff9d-fed2-43aa-ab6d-457525de1a88"));
                entity.Property(p => p.FecReg).IsRequired().HasDefaultValueSql("GETUTCDATE()").ValueGeneratedOnAdd();
                entity.Property(p => p.FecMod).HasDefaultValueSql("GETUTCDATE()").ValueGeneratedOnUpdateSometimes();
                entity.Property(p => p.Vigente).IsRequired().HasDefaultValue(true);
            });

            // Configuración de la relación muchos-a-muchos Usuario-Rol
            modelBuilder.Entity<UsuarioRol>(entity =>
            {
                entity.HasKey(ur => new { ur.UsuarioId, ur.RolId });

                entity.HasOne(ur => ur.Usuario).WithMany(u => u.UsuarioRoles).HasForeignKey(ur => ur.UsuarioId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(ur => ur.Rol).WithMany(r => r.UsuarioRoles).HasForeignKey(ur => ur.RolId).OnDelete(DeleteBehavior.Cascade);

                entity.Property(ur => ur.IdUsuReg).IsRequired().HasDefaultValue(Guid.Parse("4c86ff9d-fed2-43aa-ab6d-457525de1a88"));
                entity.Property(ur => ur.FecReg).IsRequired().HasDefaultValueSql("GETUTCDATE()").ValueGeneratedOnAdd();
                entity.Property(ur => ur.FecMod).HasDefaultValueSql("GETUTCDATE()").ValueGeneratedOnUpdate();
                entity.Property(ur => ur.Vigente).IsRequired().HasDefaultValue(true);
            });

            // Configuración de la relación muchos-a-muchos Rol-Permiso
            modelBuilder.Entity<RolPermiso>(entity =>
            {
                entity.HasKey(rp => new { rp.RolId, rp.PermisoId });

                entity.HasOne(rp => rp.Rol).WithMany(r => r.RolPermisos).HasForeignKey(rp => rp.RolId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(rp => rp.Permiso).WithMany(p => p.RolPermisos).HasForeignKey(rp => rp.PermisoId).OnDelete(DeleteBehavior.Cascade);

                entity.Property(rp => rp.IdUsuReg).IsRequired().HasDefaultValue(Guid.Parse("4c86ff9d-fed2-43aa-ab6d-457525de1a88"));
                entity.Property(rp => rp.FecReg).IsRequired().HasDefaultValueSql("GETUTCDATE()").ValueGeneratedOnAdd();
                entity.Property(rp => rp.FecMod).HasDefaultValueSql("GETUTCDATE()").ValueGeneratedOnUpdate();
                entity.Property(rp => rp.Vigente).IsRequired().HasDefaultValue(true);
            });
        }
    }
}