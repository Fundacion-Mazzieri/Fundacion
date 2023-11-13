using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fundacion.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
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
                name: "Provincias",
                columns: table => new
                {
                    pvId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pvDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincias", x => x.pvId);
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
                name: "Localidades",
                columns: table => new
                {
                    lcId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pvId = table.Column<int>(type: "int", nullable: false),
                    lcDescripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localidades", x => x.lcId);
                    table.ForeignKey(
                        name: "FK_Localidades_Provincias",
                        column: x => x.pvId,
                        principalTable: "Provincias",
                        principalColumn: "pvId",
                        onDelete: ReferentialAction.Cascade);
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
                    usLocalidad = table.Column<int>(type: "int", nullable: false),
                    usProvincia = table.Column<int>(type: "int", nullable: false),
                    usEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    usTelefono = table.Column<long>(type: "bigint", nullable: true),
                    usContrasena = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    roId = table.Column<int>(type: "int", nullable: false),
                    usActivo = table.Column<bool>(type: "bit", nullable: false),
                    token_recovery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date_created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.usId);
                    table.ForeignKey(
                        name: "FK_Usuarios_Localidades",
                        column: x => x.usLocalidad,
                        principalTable: "Localidades",
                        principalColumn: "lcId");
                    table.ForeignKey(
                        name: "FK_Usuarios_Provincias",
                        column: x => x.usProvincia,
                        principalTable: "Provincias",
                        principalColumn: "pvId");
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
                    EsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    esDescripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    tuId = table.Column<int>(type: "int", nullable: false),
                    usId = table.Column<int>(type: "int", nullable: false),
                    esActivo = table.Column<bool>(type: "bit", nullable: false),
                    caId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Espacios", x => x.EsId);
                    table.ForeignKey(
                        name: "FK_Espacios_Categorias",
                        column: x => x.caId,
                        principalTable: "Categorias",
                        principalColumn: "caId",
                        onDelete: ReferentialAction.Cascade);
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
                    asIngreso = table.Column<DateTime>(type: "datetime", nullable: false),
                    asEgreso = table.Column<DateTime>(type: "datetime", nullable: false),
                    asPresent = table.Column<bool>(type: "bit", nullable: false),
                    asCantHsRedondeo = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistencias", x => x.asiId);
                    table.ForeignKey(
                        name: "FK_Asistencias_Espacios",
                        column: x => x.esId,
                        principalTable: "Espacios",
                        principalColumn: "EsId");
                });

            migrationBuilder.CreateTable(
                name: "Subespacios",
                columns: table => new
                {
                    seId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    esId = table.Column<int>(type: "int", nullable: false),
                    auId = table.Column<int>(type: "int", nullable: false),
                    seDia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    seHora = table.Column<TimeSpan>(type: "time", maxLength: 10, nullable: true),
                    seCantHs = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subespacios", x => x.seId);
                    table.ForeignKey(
                        name: "FK_Subespacios_Aulas",
                        column: x => x.auId,
                        principalTable: "Aulas",
                        principalColumn: "auId");
                    table.ForeignKey(
                        name: "FK_Subespacios_Espacios",
                        column: x => x.esId,
                        principalTable: "Espacios",
                        principalColumn: "EsId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_esId",
                table: "Asistencias",
                column: "esId");

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
                name: "IX_Localidades_pvId",
                table: "Localidades",
                column: "pvId");

            migrationBuilder.CreateIndex(
                name: "IX_Subespacios_auId",
                table: "Subespacios",
                column: "auId");

            migrationBuilder.CreateIndex(
                name: "IX_Subespacios_esId",
                table: "Subespacios",
                column: "esId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_roId",
                table: "Usuarios",
                column: "roId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_usLocalidad",
                table: "Usuarios",
                column: "usLocalidad");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_usProvincia",
                table: "Usuarios",
                column: "usProvincia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asistencias");

            migrationBuilder.DropTable(
                name: "Subespacios");

            migrationBuilder.DropTable(
                name: "Aulas");

            migrationBuilder.DropTable(
                name: "Espacios");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Localidades");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Provincias");
        }
    }
}
