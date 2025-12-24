using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ODEVDAGITIM06.Migrations
{
    /// <inheritdoc />
    public partial class TeslimOgrenciIliskisi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OgrenciId",
                table: "Teslimler",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teslimler_OgrenciId",
                table: "Teslimler",
                column: "OgrenciId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teslimler_AspNetUsers_OgrenciId",
                table: "Teslimler",
                column: "OgrenciId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teslimler_AspNetUsers_OgrenciId",
                table: "Teslimler");

            migrationBuilder.DropIndex(
                name: "IX_Teslimler_OgrenciId",
                table: "Teslimler");

            migrationBuilder.DropColumn(
                name: "OgrenciId",
                table: "Teslimler");
        }
    }
}
