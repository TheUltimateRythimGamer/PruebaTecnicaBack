using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entity.Migrations
{
    /// <inheritdoc />
    public partial class MyFirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tiendas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sucursal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tiendas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DetalleClienteArticulo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleClienteArticulo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleClienteArticulo_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleArticuloTienda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TiendaId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleArticuloTienda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleArticuloTienda_Tiendas_TiendaId",
                        column: x => x.TiendaId,
                        principalTable: "Tiendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<float>(type: "real", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    DetalleArticuloTiendaId = table.Column<int>(type: "int", nullable: true),
                    DetalleClienteArticuloId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articulos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articulos_DetalleArticuloTienda_DetalleArticuloTiendaId",
                        column: x => x.DetalleArticuloTiendaId,
                        principalTable: "DetalleArticuloTienda",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Articulos_DetalleClienteArticulo_DetalleClienteArticuloId",
                        column: x => x.DetalleClienteArticuloId,
                        principalTable: "DetalleClienteArticulo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_DetalleArticuloTiendaId",
                table: "Articulos",
                column: "DetalleArticuloTiendaId");

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_DetalleClienteArticuloId",
                table: "Articulos",
                column: "DetalleClienteArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleArticuloTienda_TiendaId",
                table: "DetalleArticuloTienda",
                column: "TiendaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleClienteArticulo_ClienteId",
                table: "DetalleClienteArticulo",
                column: "ClienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articulos");

            migrationBuilder.DropTable(
                name: "DetalleArticuloTienda");

            migrationBuilder.DropTable(
                name: "DetalleClienteArticulo");

            migrationBuilder.DropTable(
                name: "Tiendas");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
