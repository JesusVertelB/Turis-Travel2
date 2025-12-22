using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using Turis_Travel2.Models;

namespace Turis_Travel2.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AsignacionConductore> AsignacionConductores { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Conductore> Conductores { get; set; }

    public virtual DbSet<Contrato> Contratos { get; set; }

    public virtual DbSet<Destino> Destinos { get; set; }

    public virtual DbSet<EfmigrationsHistory> EfmigrationsHistories { get; set; }

    public virtual DbSet<Itinerario> Itinerarios { get; set; }

    public virtual DbSet<Modulo> Modulos { get; set; }

    public virtual DbSet<Notificacione> Notificaciones { get; set; }

    public virtual DbSet<PaquetesTuristico> PaquetesTuristicos { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<RecuperacionContrasena> RecuperacionContrasenas { get; set; }

    public virtual DbSet<Reporte> Reportes { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Retroalimentacion> Retroalimentacions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Transporte> Transportes { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=20.51.216.86;database=turistraveldb;user=turistravel;password=TuristravelDb2025*", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.44-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<AsignacionConductore>(entity =>
        {
            entity.HasKey(e => e.IdAsignacion).HasName("PRIMARY");

            entity.ToTable("Asignacion_Conductores");

            entity.HasIndex(e => e.IdConductor, "ID_conductor");

            entity.HasIndex(e => e.IdTransporte, "ID_transporte");

            entity.Property(e => e.IdAsignacion).HasColumnName("ID_asignacion");
            entity.Property(e => e.FechaAsignacion).HasColumnName("Fecha_asignacion");
            entity.Property(e => e.IdConductor).HasColumnName("ID_conductor");
            entity.Property(e => e.IdTransporte).HasColumnName("ID_transporte");

            entity.HasOne(d => d.IdConductorNavigation).WithMany(p => p.AsignacionConductores)
                .HasForeignKey(d => d.IdConductor)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Asignacion_Conductores_ibfk_1");

            entity.HasOne(d => d.IdTransporteNavigation).WithMany(p => p.AsignacionConductores)
                .HasForeignKey(d => d.IdTransporte)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Asignacion_Conductores_ibfk_2");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PRIMARY");

            entity.Property(e => e.IdCliente).HasColumnName("ID_cliente");
            entity.Property(e => e.Cedula).HasMaxLength(50);
            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'activo'");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("Fecha_registro");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(50);
        });

        modelBuilder.Entity<Conductore>(entity =>
        {
            entity.HasKey(e => e.IdConductor).HasName("PRIMARY");

            entity.Property(e => e.IdConductor).HasColumnName("ID_conductor");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'activo'");
            entity.Property(e => e.Licencia).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Contrato>(entity =>
        {
            entity.HasKey(e => e.IdContrato).HasName("PRIMARY");

            entity.HasIndex(e => e.IdProveedor, "ID_proveedor");

            entity.Property(e => e.IdContrato).HasColumnName("ID_contrato");
            entity.Property(e => e.Condiciones).HasColumnType("text");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'activo'");
            entity.Property(e => e.FechaFin).HasColumnName("Fecha_fin");
            entity.Property(e => e.FechaInicio).HasColumnName("Fecha_inicio");
            entity.Property(e => e.IdProveedor).HasColumnName("ID_proveedor");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Contratos)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("Contratos_ibfk_1");
        });

        modelBuilder.Entity<Destino>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Categoria).HasMaxLength(45);
            entity.Property(e => e.Pais).HasMaxLength(50);
        });

        modelBuilder.Entity<EfmigrationsHistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__EFMigrationsHistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<Itinerario>(entity =>
        {
            entity.HasKey(e => e.IdItinerario).HasName("PRIMARY");

            entity.HasIndex(e => e.IdPaquete, "ID_paquete");

            entity.HasIndex(e => e.NombreItinerario, "idx_itinerario_nombre");

            entity.Property(e => e.IdItinerario).HasColumnName("ID_itinerario");
            entity.Property(e => e.Actividades).HasMaxLength(250);
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.DuracionDias).HasColumnName("Duracion_dias");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'borrador'");
            entity.Property(e => e.FechaFin).HasColumnName("Fecha_fin");
            entity.Property(e => e.FechaInicio).HasColumnName("Fecha_inicio");
            entity.Property(e => e.IdPaquete).HasColumnName("ID_paquete");
            entity.Property(e => e.NombreItinerario)
                .HasMaxLength(100)
                .HasColumnName("Nombre_itinerario");
            entity.Property(e => e.PrecioPorPersona)
                .HasPrecision(10, 2)
                .HasColumnName("Precio_por_persona");
            entity.Property(e => e.TipoItinerario)
                .HasMaxLength(50)
                .HasColumnName("Tipo_itinerario");

            entity.HasOne(d => d.IdPaqueteNavigation).WithMany(p => p.Itinerarios)
                .HasForeignKey(d => d.IdPaquete)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Itinerarios_ibfk_1");
        });

        modelBuilder.Entity<Modulo>(entity =>
        {
            entity.HasKey(e => e.IdModulo).HasName("PRIMARY");

            entity.HasIndex(e => e.NombreModulo, "Nombre_modulo").IsUnique();

            entity.Property(e => e.IdModulo).HasColumnName("ID_modulo");
            entity.Property(e => e.Descripcion).HasMaxLength(250);
            entity.Property(e => e.NombreModulo)
                .HasMaxLength(80)
                .HasColumnName("Nombre_modulo");
        });

        modelBuilder.Entity<Notificacione>(entity =>
        {
            entity.HasKey(e => e.IdNotificacion).HasName("PRIMARY");

            entity.HasIndex(e => e.IdReserva, "ID_reserva");

            entity.Property(e => e.IdNotificacion).HasColumnName("ID_notificacion");
            entity.Property(e => e.Destinatario).HasMaxLength(100);
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'enviada'");
            entity.Property(e => e.FechaEnvio)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("Fecha_envio");
            entity.Property(e => e.IdReserva).HasColumnName("ID_reserva");
            entity.Property(e => e.Tipo).HasMaxLength(50);

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.Notificaciones)
                .HasForeignKey(d => d.IdReserva)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Notificaciones_ibfk_1");
        });

        modelBuilder.Entity<PaquetesTuristico>(entity =>
        {
            entity.HasKey(e => e.IdPaquete).HasName("PRIMARY");

            entity.ToTable("Paquetes_Turisticos");

            entity.HasIndex(e => e.NombrePaquete, "idx_paquete_nombre").IsUnique();

            entity.Property(e => e.IdPaquete).HasColumnName("ID_paquete");
            entity.Property(e => e.CapacidadMaxima).HasColumnName("Capacidad_maxima");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'borrador'");
            entity.Property(e => e.FechaActualizacion)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("datetime")
                .HasColumnName("Fecha_actualizacion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("Fecha_creacion");
            entity.Property(e => e.FechaFin).HasColumnName("Fecha_fin");
            entity.Property(e => e.FechaInicio).HasColumnName("Fecha_inicio");
            entity.Property(e => e.NombrePaquete)
                .HasMaxLength(100)
                .HasColumnName("Nombre_paquete");
            entity.Property(e => e.PrecioBase)
                .HasPrecision(10, 2)
                .HasColumnName("Precio_base");

            entity.HasMany(d => d.IdServicios).WithMany(p => p.IdPaquetes)
                .UsingEntity<Dictionary<string, object>>(
                    "PaqueteServicio",
                    r => r.HasOne<Servicio>().WithMany()
                        .HasForeignKey("IdServicio")
                        .HasConstraintName("Paquete_Servicio_ibfk_2"),
                    l => l.HasOne<PaquetesTuristico>().WithMany()
                        .HasForeignKey("IdPaquete")
                        .HasConstraintName("Paquete_Servicio_ibfk_1"),
                    j =>
                    {
                        j.HasKey("IdPaquete", "IdServicio")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("Paquete_Servicio");
                        j.HasIndex(new[] { "IdServicio" }, "ID_servicio");
                        j.IndexerProperty<int>("IdPaquete").HasColumnName("ID_paquete");
                        j.IndexerProperty<int>("IdServicio").HasColumnName("ID_servicio");
                    });
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.IdPermiso).HasName("PRIMARY");

            entity.HasIndex(e => e.IdModulo, "ID_modulo");

            entity.HasIndex(e => e.IdRol, "ID_rol");

            entity.Property(e => e.IdPermiso).HasColumnName("ID_permiso");
            entity.Property(e => e.EstadoPermiso)
                .HasDefaultValueSql("'1'")
                .HasColumnName("Estado_permiso");
            entity.Property(e => e.IdModulo).HasColumnName("ID_modulo");
            entity.Property(e => e.IdRol).HasColumnName("ID_rol");

            entity.HasOne(d => d.IdModuloNavigation).WithMany(p => p.Permisos)
                .HasForeignKey(d => d.IdModulo)
                .HasConstraintName("Permisos_ibfk_2");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Permisos)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("Permisos_ibfk_1");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PRIMARY");

            entity.HasIndex(e => e.Nombre, "idx_proveedor_nombre");

            entity.Property(e => e.IdProveedor).HasColumnName("ID_proveedor");
            entity.Property(e => e.Contacto).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(250);
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'activo'");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Tipo).HasMaxLength(50);
        });

        modelBuilder.Entity<RecuperacionContrasena>(entity =>
        {
            entity.HasKey(e => e.IdRecuperacion).HasName("PRIMARY");

            entity.ToTable("Recuperacion_Contrasena");

            entity.HasIndex(e => e.IdUsuario, "ID_usuario");

            entity.HasIndex(e => e.Token, "Token").IsUnique();

            entity.Property(e => e.IdRecuperacion).HasColumnName("ID_recuperacion");
            entity.Property(e => e.IdUsuario).HasColumnName("ID_usuario");
            entity.Property(e => e.Token).HasMaxLength(250);
            entity.Property(e => e.Usado).HasDefaultValueSql("'0'");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.RecuperacionContrasenas)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("Recuperacion_Contrasena_ibfk_1");
        });

        modelBuilder.Entity<Reporte>(entity =>
        {
            entity.HasKey(e => e.IdReporte).HasName("PRIMARY");

            entity.Property(e => e.IdReporte).HasColumnName("ID_reporte");
            entity.Property(e => e.Datos).HasColumnType("text");
            entity.Property(e => e.FechaGeneracion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("Fecha_generacion");
            entity.Property(e => e.Tipo).HasMaxLength(50);
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdReserva).HasName("PRIMARY");

            entity.HasIndex(e => e.IdItinerario, "ID_itinerario");

            entity.HasIndex(e => e.IdPaquete, "ID_paquete");

            entity.HasIndex(e => e.IdTransporte, "ID_transporte");

            entity.HasIndex(e => e.IdCliente, "idx_reserva_usuario");

            entity.Property(e => e.IdReserva).HasColumnName("ID_reserva");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'pendiente'");
            entity.Property(e => e.FechaSolicitud)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("Fecha_solicitud");
            entity.Property(e => e.IdCliente).HasColumnName("ID_cliente");
            entity.Property(e => e.IdItinerario).HasColumnName("ID_itinerario");
            entity.Property(e => e.IdPaquete).HasColumnName("ID_paquete");
            entity.Property(e => e.IdTransporte).HasColumnName("ID_transporte");
            entity.Property(e => e.NumeroPasajeros).HasColumnName("Numero_pasajeros");
            entity.Property(e => e.PrecioTotal)
                .HasPrecision(10, 2)
                .HasColumnName("Precio_total");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("fk_reservas_cliente");

            entity.HasOne(d => d.IdItinerarioNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdItinerario)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Reservas_ibfk_3");

            entity.HasOne(d => d.IdPaqueteNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdPaquete)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Reservas_ibfk_2");

            entity.HasOne(d => d.IdTransporteNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdTransporte)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Reservas_ibfk_4");
        });

        modelBuilder.Entity<Retroalimentacion>(entity =>
        {
            entity.HasKey(e => e.IdRetroalimentacion).HasName("PRIMARY");

            entity.ToTable("Retroalimentacion");

            entity.HasIndex(e => e.IdReserva, "ID_reserva");

            entity.Property(e => e.IdRetroalimentacion).HasColumnName("ID_retroalimentacion");
            entity.Property(e => e.Anonimo).HasDefaultValueSql("'0'");
            entity.Property(e => e.Comentario).HasColumnType("text");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.IdReserva).HasColumnName("ID_reserva");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.Retroalimentacions)
                .HasForeignKey(d => d.IdReserva)
                .HasConstraintName("Retroalimentacion_ibfk_1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PRIMARY");

            entity.HasIndex(e => e.NombreRol, "Nombre_rol").IsUnique();

            entity.Property(e => e.IdRol).HasColumnName("ID_rol");
            entity.Property(e => e.EstadoRol)
                .HasDefaultValueSql("'1'")
                .HasColumnName("Estado_rol");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(80)
                .HasColumnName("Nombre_rol");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio).HasName("PRIMARY");

            entity.HasIndex(e => e.NombreServicio, "Nombre_servicio").IsUnique();

            entity.Property(e => e.IdServicio).HasColumnName("ID_servicio");
            entity.Property(e => e.Descripcion).HasMaxLength(250);
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'activo'");
            entity.Property(e => e.NombreServicio)
                .HasMaxLength(100)
                .HasColumnName("Nombre_servicio");
        });

        modelBuilder.Entity<Transporte>(entity =>
        {
            entity.HasKey(e => e.IdTransporte).HasName("PRIMARY");

            entity.HasIndex(e => e.IdPaquete, "ID_paquete");

            entity.Property(e => e.IdTransporte).HasColumnName("ID_transporte");
            entity.Property(e => e.Disponibilidad).HasDefaultValueSql("'1'");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'activo'");
            entity.Property(e => e.IdPaquete).HasColumnName("ID_paquete");
            entity.Property(e => e.TipoVehiculo)
                .HasMaxLength(50)
                .HasColumnName("Tipo_vehiculo");

            entity.HasOne(d => d.IdPaqueteNavigation).WithMany(p => p.Transportes)
                .HasForeignKey(d => d.IdPaquete)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Transportes_ibfk_1");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.HasIndex(e => e.Correo, "Correo").IsUnique();

            entity.HasIndex(e => e.IdRol, "ID_rol");

            entity.Property(e => e.IdUsuario).HasColumnName("ID_usuario");
            entity.Property(e => e.Contrasena).HasMaxLength(250);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Estado).HasDefaultValueSql("'1'");
            entity.Property(e => e.FechaActualizacion)
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnType("datetime")
                .HasColumnName("Fecha_actualizacion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("Fecha_creacion");
            entity.Property(e => e.IdRol).HasColumnName("ID_rol");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .HasColumnName("Nombre_usuario");
            entity.Property(e => e.Observacion).HasMaxLength(100);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Usuarios_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
