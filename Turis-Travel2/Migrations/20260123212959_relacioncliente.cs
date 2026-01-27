using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Turis_Travel2.Migrations
{
    /// <inheritdoc />
    public partial class relacioncliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "ID_reserva1",
                table: "Retroalimentacion",
                newName: "ID_reserva2");

            migrationBuilder.AddColumn<int>(
                name: "IdRol",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    ID_pago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_reserva = table.Column<int>(type: "int", nullable: false),
                    MetodoPago = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Monto = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Estado = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, defaultValueSql: "'pendiente'", collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaPago = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_pago);
                    table.ForeignKey(
                        name: "fk_pagos_reserva",
                        column: x => x.ID_reserva,
                        principalTable: "Reservas",
                        principalColumn: "ID_reserva",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_IdRol",
                table: "Clientes",
                column: "IdRol",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ID_reserva1",
                table: "Pagos",
                column: "ID_reserva");

            migrationBuilder.AddForeignKey(
                name: "fk_cliente_rol",
                table: "Clientes",
                column: "IdRol",
                principalTable: "Roles",
                principalColumn: "ID_rol",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cliente_rol",
                table: "Clientes");

            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_IdRol",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "IdRol",
                table: "Clientes");

            migrationBuilder.RenameIndex(
                name: "ID_reserva2",
                table: "Retroalimentacion",
                newName: "ID_reserva1");
        }
    }
}
