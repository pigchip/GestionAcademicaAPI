using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionAcademicaAPI.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureComentarioCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Materias_IdMateria",
                table: "Comentarios");

            migrationBuilder.RenameColumn(
                name: "IdMateria",
                table: "Comentarios",
                newName: "IdSolicitud");

            migrationBuilder.RenameIndex(
                name: "IX_Comentarios_IdMateria",
                table: "Comentarios",
                newName: "IX_Comentarios_IdSolicitud");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Solicitudes_IdSolicitud",
                table: "Comentarios",
                column: "IdSolicitud",
                principalTable: "Solicitudes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Solicitudes_IdSolicitud",
                table: "Comentarios");

            migrationBuilder.RenameColumn(
                name: "IdSolicitud",
                table: "Comentarios",
                newName: "IdMateria");

            migrationBuilder.RenameIndex(
                name: "IX_Comentarios_IdSolicitud",
                table: "Comentarios",
                newName: "IX_Comentarios_IdMateria");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Materias_IdMateria",
                table: "Comentarios",
                column: "IdMateria",
                principalTable: "Materias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
