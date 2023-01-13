using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entity.Migrations
{
    /// <inheritdoc />
    public partial class CambiosRelacionales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_DetalleArticuloTienda_DetalleArticuloTiendaId",
                table: "Articulos");

            migrationBuilder.DropTable(
                name: "DetalleArticuloTienda");

            migrationBuilder.DropIndex(
                name: "IX_Articulos_DetalleArticuloTiendaId",
                table: "Articulos");

            migrationBuilder.DropColumn(
                name: "DetalleArticuloTiendaId",
                table: "Articulos");

            migrationBuilder.AddColumn<int>(
                name: "TiendaId",
                table: "Articulos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_TiendaId",
                table: "Articulos",
                column: "TiendaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_Tiendas_TiendaId",
                table: "Articulos",
                column: "TiendaId",
                principalTable: "Tiendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articulos_Tiendas_TiendaId",
                table: "Articulos");

            migrationBuilder.DropIndex(
                name: "IX_Articulos_TiendaId",
                table: "Articulos");

            migrationBuilder.DropColumn(
                name: "TiendaId",
                table: "Articulos");

            migrationBuilder.AddColumn<int>(
                name: "DetalleArticuloTiendaId",
                table: "Articulos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DetalleArticuloTienda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TiendaId = table.Column<int>(type: "int", nullable: false),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_Articulos_DetalleArticuloTiendaId",
                table: "Articulos",
                column: "DetalleArticuloTiendaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleArticuloTienda_TiendaId",
                table: "DetalleArticuloTienda",
                column: "TiendaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articulos_DetalleArticuloTienda_DetalleArticuloTiendaId",
                table: "Articulos",
                column: "DetalleArticuloTiendaId",
                principalTable: "DetalleArticuloTienda",
                principalColumn: "Id");
        }
    }
}
