using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetWeb.Migrations
{
    /// <inheritdoc />
    public partial class DB2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Evenement_IDVol",
                table: "Evenement",
                column: "IDVol");

            migrationBuilder.AddForeignKey(
                name: "FK_Evenement_Vol_IDVol",
                table: "Evenement",
                column: "IDVol",
                principalTable: "Vol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evenement_Vol_IDVol",
                table: "Evenement");

            migrationBuilder.DropIndex(
                name: "IX_Evenement_IDVol",
                table: "Evenement");
        }
    }
}
