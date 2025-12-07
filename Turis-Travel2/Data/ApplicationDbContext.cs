using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using Turis_Travel2.Models.Scaffolded;

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

    public virtual DbSet<Asignacion_Conductore> Asignacion_Conductores { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Conductore> Conductores { get; set; }

    public virtual DbSet<Contrato> Contratos { get; set; }

    public virtual DbSet<Itinerario> Itinerarios { get; set; }

    public virtual DbSet<Modulo> Modulos { get; set; }

    public virtual DbSet<Notificacione> Notificaciones { get; set; }

    public virtual DbSet<Paquetes_Turistico> Paquetes_Turisticos { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<Recuperacion_Contrasena> Recuperacion_Contrasenas { get; set; }

    public virtual DbSet<Reporte> Reportes { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Retroalimentacion> Retroalimentacions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Transporte> Transportes { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=20.81.230.41;port=3306;database=turistraveldb;user=turistravel;password=Turistravel2025*", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.44-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Asignacion_Conductore>(entity =>
        {
            entity.HasKey(e => e.ID_asignacion).HasName("PRIMARY");

            entity.HasOne(d => d.ID_conductorNavigation).WithMany(p => p.Asignacion_Conductores)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Asignacion_Conductores_ibfk_1");

            entity.HasOne(d => d.ID_transporteNavigation).WithMany(p => p.Asignacion_Conductores)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Asignacion_Conductores_ibfk_2");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ID_cliente).HasName("PRIMARY");

            entity.Property(e => e.Estado).HasDefaultValueSql("'activo'");
            entity.Property(e => e.Fecha_registro).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Conductore>(entity =>
        {
            entity.HasKey(e => e.ID_conductor).HasName("PRIMARY");

            entity.Property(e => e.Estado).HasDefaultValueSql("'activo'");
        });

        modelBuilder.Entity<Contrato>(entity =>
        {
            entity.HasKey(e => e.ID_contrato).HasName("PRIMARY");

            entity.Property(e => e.Estado).HasDefaultValueSql("'activo'");

            entity.HasOne(d => d.ID_proveedorNavigation).WithMany(p => p.Contratos).HasConstraintName("Contratos_ibfk_1");
        });

        modelBuilder.Entity<Itinerario>(entity =>
        {
            entity.HasKey(e => e.ID_itinerario).HasName("PRIMARY");

            entity.Property(e => e.Estado).HasDefaultValueSql("'borrador'");

            entity.HasOne(d => d.ID_paqueteNavigation).WithMany(p => p.Itinerarios)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Itinerarios_ibfk_1");
        });

        modelBuilder.Entity<Modulo>(entity =>
        {
            entity.HasKey(e => e.ID_modulo).HasName("PRIMARY");
        });

        modelBuilder.Entity<Notificacione>(entity =>
        {
            entity.HasKey(e => e.ID_notificacion).HasName("PRIMARY");

            entity.Property(e => e.Estado).HasDefaultValueSql("'enviada'");
            entity.Property(e => e.Fecha_envio).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.ID_reservaNavigation).WithMany(p => p.Notificaciones)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Notificaciones_ibfk_1");
        });

        modelBuilder.Entity<Paquetes_Turistico>(entity =>
        {
            entity.HasKey(e => e.ID_paquete).HasName("PRIMARY");

            entity.Property(e => e.Estado).HasDefaultValueSql("'borrador'");
            entity.Property(e => e.Fecha_actualizacion).ValueGeneratedOnAddOrUpdate();
            entity.Property(e => e.Fecha_creacion).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasMany(d => d.ID_servicios).WithMany(p => p.ID_paquetes)
                .UsingEntity<Dictionary<string, object>>(
                    "Paquete_Servicio",
                    r => r.HasOne<Servicio>().WithMany()
                        .HasForeignKey("ID_servicio")
                        .HasConstraintName("Paquete_Servicio_ibfk_2"),
                    l => l.HasOne<Paquetes_Turistico>().WithMany()
                        .HasForeignKey("ID_paquete")
                        .HasConstraintName("Paquete_Servicio_ibfk_1"),
                    j =>
                    {
                        j.HasKey("ID_paquete", "ID_servicio")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("Paquete_Servicio");
                        j.HasIndex(new[] { "ID_servicio" }, "ID_servicio");
                    });
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.ID_permiso).HasName("PRIMARY");

            entity.Property(e => e.Estado_permiso).HasDefaultValueSql("'1'");

            entity.HasOne(d => d.ID_moduloNavigation).WithMany(p => p.Permisos).HasConstraintName("Permisos_ibfk_2");

            entity.HasOne(d => d.ID_rolNavigation).WithMany(p => p.Permisos).HasConstraintName("Permisos_ibfk_1");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.ID_proveedor).HasName("PRIMARY");

            entity.Property(e => e.Estado).HasDefaultValueSql("'activo'");
        });

        modelBuilder.Entity<Recuperacion_Contrasena>(entity =>
        {
            entity.HasKey(e => e.ID_recuperacion).HasName("PRIMARY");

            entity.Property(e => e.Usado).HasDefaultValueSql("'0'");

            entity.HasOne(d => d.ID_usuarioNavigation).WithMany(p => p.Recuperacion_Contrasenas).HasConstraintName("Recuperacion_Contrasena_ibfk_1");
        });

        modelBuilder.Entity<Reporte>(entity =>
        {
            entity.HasKey(e => e.ID_reporte).HasName("PRIMARY");

            entity.Property(e => e.Fecha_generacion).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.ID_reserva).HasName("PRIMARY");

            entity.Property(e => e.Estado).HasDefaultValueSql("'pendiente'");
            entity.Property(e => e.Fecha_solicitud).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.ID_clienteNavigation).WithMany(p => p.Reservas).HasConstraintName("fk_reservas_cliente");

            entity.HasOne(d => d.ID_itinerarioNavigation).WithMany(p => p.Reservas)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Reservas_ibfk_3");

            entity.HasOne(d => d.ID_paqueteNavigation).WithMany(p => p.Reservas)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Reservas_ibfk_2");

            entity.HasOne(d => d.ID_transporteNavigation).WithMany(p => p.Reservas)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Reservas_ibfk_4");
        });

        modelBuilder.Entity<Retroalimentacion>(entity =>
        {
            entity.HasKey(e => e.ID_retroalimentacion).HasName("PRIMARY");

            entity.Property(e => e.Anonimo).HasDefaultValueSql("'0'");
            entity.Property(e => e.Fecha).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.ID_reservaNavigation).WithMany(p => p.Retroalimentacions).HasConstraintName("Retroalimentacion_ibfk_1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.ID_rol).HasName("PRIMARY");

            entity.Property(e => e.Estado_rol).HasDefaultValueSql("'1'");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.ID_servicio).HasName("PRIMARY");

            entity.Property(e => e.Estado).HasDefaultValueSql("'activo'");
        });

        modelBuilder.Entity<Transporte>(entity =>
        {
            entity.HasKey(e => e.ID_transporte).HasName("PRIMARY");

            entity.Property(e => e.Disponibilidad).HasDefaultValueSql("'1'");
            entity.Property(e => e.Estado).HasDefaultValueSql("'activo'");

            entity.HasOne(d => d.ID_paqueteNavigation).WithMany(p => p.Transportes)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Transportes_ibfk_1");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.ID_usuario).HasName("PRIMARY");

            entity.Property(e => e.Estado).HasDefaultValueSql("'1'");
            entity.Property(e => e.Fecha_actualizacion).ValueGeneratedOnAddOrUpdate();
            entity.Property(e => e.Fecha_creacion).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.ID_rolNavigation).WithMany(p => p.Usuarios)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Usuarios_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
