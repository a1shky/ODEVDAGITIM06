using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ODEVDAGITIM06.Migrations
{
    /// <inheritdoc />
    public partial class OdevOgrenciIliskisi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OgrenciId",
                table: "Odevler",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Odevler_OgrenciId",
                table: "Odevler",
                column: "OgrenciId");

            migrationBuilder.AddForeignKey(
                name: "FK_Odevler_AspNetUsers_OgrenciId",
                table: "Odevler",
                column: "OgrenciId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Odevler_AspNetUsers_OgrenciId",
                table: "Odevler");

            migrationBuilder.DropIndex(
                name: "IX_Odevler_OgrenciId",
                table: "Odevler");

            migrationBuilder.DropColumn(
                name: "OgrenciId",
                table: "Odevler");
        }
    }
}
