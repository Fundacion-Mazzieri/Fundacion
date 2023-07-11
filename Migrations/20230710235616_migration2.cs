using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fundacion.Migrations
{
    /// <inheritdoc />
    public partial class migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aulas",
                columns: table => new
                {
                    auId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    auDescripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aulas", x => x.auId);
                });

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    caId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    caDescripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    caValorHora = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.caId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    roId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    roDenominacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.roId);
                });

            migrationBuilder.CreateTable(
                name: "Turnos",
                columns: table => new
                {
                    tuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tuDescripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trunos", x => x.tuId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    usId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usDNI = table.Column<long>(type: "bigint", nullable: false),
                    usApellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    usNombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    usDireccion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    usLocalidad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    usProvincia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    usEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    usTelefono = table.Column<long>(type: "bigint", nullable: true),
                    usContrasena = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    roId = table.Column<int>(type: "int", nullable: false),
                    usActivo = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.usId);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles",
                        column: x => x.roId,
                        principalTable: "Roles",
                        principalColumn: "roId");
                });

            migrationBuilder.CreateTable(
                name: "Espacios",
                columns: table => new
                {
                    esId = table.Column<int>(type: "int", nullable: false),
                    esDescripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    auId = table.Column<int>(type: "int", nullable: false),
                    esDia = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    esHora = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    esCantHs = table.Column<double>(type: "float", nullable: false),
                    tuId = table.Column<int>(type: "int", nullable: false),
                    usId = table.Column<int>(type: "int", nullable: false),
                    esActivo = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    caId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Espacios", x => x.esId);
                    table.ForeignKey(
                        name: "FK_Espacios_Aulas",
                        column: x => x.auId,
                        principalTable: "Aulas",
                        principalColumn: "auId");
                    table.ForeignKey(
                        name: "FK_Espacios_Categorias",
                        column: x => x.caId,
                        principalTable: "Categorias",
                        principalColumn: "caId");
                    table.ForeignKey(
                        name: "FK_Espacios_Turnos",
                        column: x => x.tuId,
                        principalTable: "Turnos",
                        principalColumn: "tuId");
                    table.ForeignKey(
                        name: "FK_Espacios_Usuarios",
                        column: x => x.usId,
                        principalTable: "Usuarios",
                        principalColumn: "usId");
                });

            migrationBuilder.CreateTable(
                name: "Asistencias",
                columns: table => new
                {
                    asiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    esId = table.Column<int>(type: "int", nullable: false),
                    asIngreso = table.Column<DateTime>(type: "datetime", nullable: true),
                    asEgreso = table.Column<DateTime>(type: "datetime", nullable: true),
                    asPresent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistencias", x => x.asiId);
                    table.ForeignKey(
                        name: "FK_Asistencias_Espacios",
                        column: x => x.esId,
                        principalTable: "Espacios",
                        principalColumn: "esId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_esId",
                table: "Asistencias",
                column: "esId");

            migrationBuilder.CreateIndex(
                name: "IX_Espacios_auId",
                table: "Espacios",
                column: "auId");

            migrationBuilder.CreateIndex(
                name: "IX_Espacios_caId",
                table: "Espacios",
                column: "caId");

            migrationBuilder.CreateIndex(
                name: "IX_Espacios_tuId",
                table: "Espacios",
                column: "tuId");

            migrationBuilder.CreateIndex(
                name: "IX_Espacios_usId",
                table: "Espacios",
                column: "usId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_roId",
                table: "Usuarios",
                column: "roId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asistencias");

            migrationBuilder.DropTable(
                name: "Espacios");

            migrationBuilder.DropTable(
                name: "Aulas");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
