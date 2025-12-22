using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Turis_Travel2.Migrations
{
    /// <inheritdoc />
    public partial class Imgpaquetes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ID_cliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cedula = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefono = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Direccion = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, defaultValueSql: "'activo'", collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_registro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_cliente);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "Conductores",
                columns: table => new
                {
                    ID_conductor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Licencia = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, defaultValueSql: "'activo'", collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_conductor);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "Destinos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Pais = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Precio = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Categoria = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImagenUrl = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<sbyte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "Modulos",
                columns: table => new
                {
                    ID_modulo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre_modulo = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_modulo);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "Paquetes_Turisticos",
                columns: table => new
                {
                    ID_paquete = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre_paquete = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Precio_base = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Fecha_inicio = table.Column<DateOnly>(type: "date", nullable: true),
                    Fecha_fin = table.Column<DateOnly>(type: "date", nullable: true),
                    Capacidad_maxima = table.Column<int>(type: "int", nullable: true),
                    Estado = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, defaultValueSql: "'borrador'", collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_creacion = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Fecha_actualizacion = table.Column<DateTime>(type: "datetime", nullable: true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    ImagenUrl = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_paquete);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    ID_proveedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tipo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Contacto = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Direccion = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, defaultValueSql: "'activo'", collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_proveedor);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "Reportes",
                columns: table => new
                {
                    ID_reporte = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Tipo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_generacion = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Datos = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_reporte);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID_rol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre_rol = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado_rol = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'1'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_rol);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    ID_servicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre_servicio = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, defaultValueSql: "'activo'", collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_servicio);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "Itinerarios",
                columns: table => new
                {
                    ID_itinerario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_paquete = table.Column<int>(type: "int", nullable: true),
                    Nombre_itinerario = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tipo_itinerario = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Actividades = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Duracion_dias = table.Column<int>(type: "int", nullable: true),
                    Fecha_inicio = table.Column<DateOnly>(type: "date", nullable: true),
                    Fecha_fin = table.Column<DateOnly>(type: "date", nullable: true),
                    Precio_por_persona = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    Estado = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, defaultValueSql: "'borrador'", collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_itinerario);
                    table.ForeignKey(
                        name: "Itinerarios_ibfk_1",
                        column: x => x.ID_paquete,
                        principalTable: "Paquetes_Turisticos",
                        principalColumn: "ID_paquete",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "Transportes",
                columns: table => new
                {
                    ID_transporte = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_paquete = table.Column<int>(type: "int", nullable: true),
                    Tipo_vehiculo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Capacidad = table.Column<int>(type: "int", nullable: true),
                    Disponibilidad = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'1'"),
                    Estado = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, defaultValueSql: "'activo'", collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_transporte);
                    table.ForeignKey(
                        name: "Transportes_ibfk_1",
                        column: x => x.ID_paquete,
                        principalTable: "Paquetes_Turisticos",
                        principalColumn: "ID_paquete",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "Contratos",
                columns: table => new
                {
                    ID_contrato = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_proveedor = table.Column<int>(type: "int", nullable: false),
                    Fecha_inicio = table.Column<DateOnly>(type: "date", nullable: false),
                    Fecha_fin = table.Column<DateOnly>(type: "date", nullable: false),
                    Condiciones = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, defaultValueSql: "'activo'", collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_contrato);
                    table.ForeignKey(
                        name: "Contratos_ibfk_1",
                        column: x => x.ID_proveedor,
                        principalTable: "Proveedores",
                        principalColumn: "ID_proveedor",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    ID_permiso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_rol = table.Column<int>(type: "int", nullable: false),
                    ID_modulo = table.Column<int>(type: "int", nullable: false),
                    Estado_permiso = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'1'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_permiso);
                    table.ForeignKey(
                        name: "Permisos_ibfk_1",
                        column: x => x.ID_rol,
                        principalTable: "Roles",
                        principalColumn: "ID_rol",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Permisos_ibfk_2",
                        column: x => x.ID_modulo,
                        principalTable: "Modulos",
                        principalColumn: "ID_modulo",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    ID_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_rol = table.Column<int>(type: "int", nullable: true),
                    Nombre_usuario = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Correo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Contrasena = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'1'"),
                    Observacion = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_creacion = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Fecha_actualizacion = table.Column<DateTime>(type: "datetime", nullable: true)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_usuario);
                    table.ForeignKey(
                        name: "Usuarios_ibfk_1",
                        column: x => x.ID_rol,
                        principalTable: "Roles",
                        principalColumn: "ID_rol",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "Paquete_Servicio",
                columns: table => new
                {
                    ID_paquete = table.Column<int>(type: "int", nullable: false),
                    ID_servicio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.ID_paquete, x.ID_servicio })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "Paquete_Servicio_ibfk_1",
                        column: x => x.ID_paquete,
                        principalTable: "Paquetes_Turisticos",
                        principalColumn: "ID_paquete",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Paquete_Servicio_ibfk_2",
                        column: x => x.ID_servicio,
                        principalTable: "Servicios",
                        principalColumn: "ID_servicio",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "Asignacion_Conductores",
                columns: table => new
                {
                    ID_asignacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_conductor = table.Column<int>(type: "int", nullable: true),
                    ID_transporte = table.Column<int>(type: "int", nullable: true),
                    Fecha_asignacion = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_asignacion);
                    table.ForeignKey(
                        name: "Asignacion_Conductores_ibfk_1",
                        column: x => x.ID_conductor,
                        principalTable: "Conductores",
                        principalColumn: "ID_conductor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Asignacion_Conductores_ibfk_2",
                        column: x => x.ID_transporte,
                        principalTable: "Transportes",
                        principalColumn: "ID_transporte",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    ID_reserva = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_cliente = table.Column<int>(type: "int", nullable: false),
                    ID_paquete = table.Column<int>(type: "int", nullable: true),
                    ID_itinerario = table.Column<int>(type: "int", nullable: true),
                    ID_transporte = table.Column<int>(type: "int", nullable: true),
                    Fecha_solicitud = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Estado = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, defaultValueSql: "'pendiente'", collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Numero_pasajeros = table.Column<int>(type: "int", nullable: true),
                    Precio_total = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_reserva);
                    table.ForeignKey(
                        name: "Reservas_ibfk_2",
                        column: x => x.ID_paquete,
                        principalTable: "Paquetes_Turisticos",
                        principalColumn: "ID_paquete",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "Reservas_ibfk_3",
                        column: x => x.ID_itinerario,
                        principalTable: "Itinerarios",
                        principalColumn: "ID_itinerario",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "Reservas_ibfk_4",
                        column: x => x.ID_transporte,
                        principalTable: "Transportes",
                        principalColumn: "ID_transporte",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_reservas_cliente",
                        column: x => x.ID_cliente,
                        principalTable: "Clientes",
                        principalColumn: "ID_cliente",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "Recuperacion_Contrasena",
                columns: table => new
                {
                    ID_recuperacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_usuario = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Expiracion = table.Column<DateOnly>(type: "date", nullable: false),
                    Usado = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'0'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_recuperacion);
                    table.ForeignKey(
                        name: "Recuperacion_Contrasena_ibfk_1",
                        column: x => x.ID_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "ID_usuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "Notificaciones",
                columns: table => new
                {
                    ID_notificacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_reserva = table.Column<int>(type: "int", nullable: true),
                    Tipo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha_envio = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Destinatario = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, defaultValueSql: "'enviada'", collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_notificacion);
                    table.ForeignKey(
                        name: "Notificaciones_ibfk_1",
                        column: x => x.ID_reserva,
                        principalTable: "Reservas",
                        principalColumn: "ID_reserva",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "Retroalimentacion",
                columns: table => new
                {
                    ID_retroalimentacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_reserva = table.Column<int>(type: "int", nullable: false),
                    Comentario = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Puntuacion = table.Column<int>(type: "int", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Anonimo = table.Column<int>(type: "int", nullable: true, defaultValueSql: "'0'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_retroalimentacion);
                    table.ForeignKey(
                        name: "Retroalimentacion_ibfk_1",
                        column: x => x.ID_reserva,
                        principalTable: "Reservas",
                        principalColumn: "ID_reserva",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateIndex(
                name: "ID_conductor",
                table: "Asignacion_Conductores",
                column: "ID_conductor");

            migrationBuilder.CreateIndex(
                name: "ID_transporte",
                table: "Asignacion_Conductores",
                column: "ID_transporte");

            migrationBuilder.CreateIndex(
                name: "ID_proveedor",
                table: "Contratos",
                column: "ID_proveedor");

            migrationBuilder.CreateIndex(
                name: "ID_paquete",
                table: "Itinerarios",
                column: "ID_paquete");

            migrationBuilder.CreateIndex(
                name: "idx_itinerario_nombre",
                table: "Itinerarios",
                column: "Nombre_itinerario");

            migrationBuilder.CreateIndex(
                name: "Nombre_modulo",
                table: "Modulos",
                column: "Nombre_modulo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ID_reserva",
                table: "Notificaciones",
                column: "ID_reserva");

            migrationBuilder.CreateIndex(
                name: "ID_servicio",
                table: "Paquete_Servicio",
                column: "ID_servicio");

            migrationBuilder.CreateIndex(
                name: "idx_paquete_nombre",
                table: "Paquetes_Turisticos",
                column: "Nombre_paquete",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ID_modulo",
                table: "Permisos",
                column: "ID_modulo");

            migrationBuilder.CreateIndex(
                name: "ID_rol",
                table: "Permisos",
                column: "ID_rol");

            migrationBuilder.CreateIndex(
                name: "idx_proveedor_nombre",
                table: "Proveedores",
                column: "Nombre");

            migrationBuilder.CreateIndex(
                name: "ID_usuario",
                table: "Recuperacion_Contrasena",
                column: "ID_usuario");

            migrationBuilder.CreateIndex(
                name: "Token",
                table: "Recuperacion_Contrasena",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ID_itinerario",
                table: "Reservas",
                column: "ID_itinerario");

            migrationBuilder.CreateIndex(
                name: "ID_paquete1",
                table: "Reservas",
                column: "ID_paquete");

            migrationBuilder.CreateIndex(
                name: "ID_transporte1",
                table: "Reservas",
                column: "ID_transporte");

            migrationBuilder.CreateIndex(
                name: "idx_reserva_usuario",
                table: "Reservas",
                column: "ID_cliente");

            migrationBuilder.CreateIndex(
                name: "ID_reserva1",
                table: "Retroalimentacion",
                column: "ID_reserva");

            migrationBuilder.CreateIndex(
                name: "Nombre_rol",
                table: "Roles",
                column: "Nombre_rol",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Nombre_servicio",
                table: "Servicios",
                column: "Nombre_servicio",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ID_paquete2",
                table: "Transportes",
                column: "ID_paquete");

            migrationBuilder.CreateIndex(
                name: "Correo",
                table: "Usuarios",
                column: "Correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ID_rol1",
                table: "Usuarios",
                column: "ID_rol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "__EFMigrationsHistory");

            migrationBuilder.DropTable(
                name: "Asignacion_Conductores");

            migrationBuilder.DropTable(
                name: "Contratos");

            migrationBuilder.DropTable(
                name: "Destinos");

            migrationBuilder.DropTable(
                name: "Notificaciones");

            migrationBuilder.DropTable(
                name: "Paquete_Servicio");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "Recuperacion_Contrasena");

            migrationBuilder.DropTable(
                name: "Reportes");

            migrationBuilder.DropTable(
                name: "Retroalimentacion");

            migrationBuilder.DropTable(
                name: "Conductores");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Servicios");

            migrationBuilder.DropTable(
                name: "Modulos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Itinerarios");

            migrationBuilder.DropTable(
                name: "Transportes");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Paquetes_Turisticos");
        }
    }
}
