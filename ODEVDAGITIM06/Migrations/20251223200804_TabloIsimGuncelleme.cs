using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ODEVDAGITIM06.Migrations
{
    /// <inheritdoc />
    public partial class TabloIsimGuncelleme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Odevler_AspNetUsers_OgrenciId",
                table: "Odevler");

            migrationBuilder.DropForeignKey(
                name: "FK_Odevler_Dersler_DersId",
                table: "Odevler");

            migrationBuilder.DropForeignKey(
                name: "FK_Teslimler_AspNetUsers_OgrenciId",
                table: "Teslimler");

            migrationBuilder.DropForeignKey(
                name: "FK_Teslimler_Odevler_OdevId",
                table: "Teslimler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teslimler",
                table: "Teslimler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Odevler",
                table: "Odevler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dersler",
                table: "Dersler");

            migrationBuilder.RenameTable(
                name: "Teslimler",
                newName: "Teslim");

            migrationBuilder.RenameTable(
                name: "Odevler",
                newName: "Odev");

            migrationBuilder.RenameTable(
                name: "Dersler",
                newName: "Ders");

            migrationBuilder.RenameIndex(
                name: "IX_Teslimler_OgrenciId",
                table: "Teslim",
                newName: "IX_Teslim_OgrenciId");

            migrationBuilder.RenameIndex(
                name: "IX_Teslimler_OdevId",
                table: "Teslim",
                newName: "IX_Teslim_OdevId");

            migrationBuilder.RenameIndex(
                name: "IX_Odevler_OgrenciId",
                table: "Odev",
                newName: "IX_Odev_OgrenciId");

            migrationBuilder.RenameIndex(
                name: "IX_Odevler_DersId",
                table: "Odev",
                newName: "IX_Odev_DersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teslim",
                table: "Teslim",
                column: "TeslimId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Odev",
                table: "Odev",
                column: "OdevId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ders",
                table: "Ders",
                column: "DersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Odev_AspNetUsers_OgrenciId",
                table: "Odev",
                column: "OgrenciId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Odev_Ders_DersId",
                table: "Odev",
                column: "DersId",
                principalTable: "Ders",
                principalColumn: "DersId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teslim_AspNetUsers_OgrenciId",
                table: "Teslim",
                column: "OgrenciId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teslim_Odev_OdevId",
                table: "Teslim",
                column: "OdevId",
                principalTable: "Odev",
                principalColumn: "OdevId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Odev_AspNetUsers_OgrenciId",
                table: "Odev");

            migrationBuilder.DropForeignKey(
                name: "FK_Odev_Ders_DersId",
                table: "Odev");

            migrationBuilder.DropForeignKey(
                name: "FK_Teslim_AspNetUsers_OgrenciId",
                table: "Teslim");

            migrationBuilder.DropForeignKey(
                name: "FK_Teslim_Odev_OdevId",
                table: "Teslim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teslim",
                table: "Teslim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Odev",
                table: "Odev");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ders",
                table: "Ders");

            migrationBuilder.RenameTable(
                name: "Teslim",
                newName: "Teslimler");

            migrationBuilder.RenameTable(
                name: "Odev",
                newName: "Odevler");

            migrationBuilder.RenameTable(
                name: "Ders",
                newName: "Dersler");

            migrationBuilder.RenameIndex(
                name: "IX_Teslim_OgrenciId",
                table: "Teslimler",
                newName: "IX_Teslimler_OgrenciId");

            migrationBuilder.RenameIndex(
                name: "IX_Teslim_OdevId",
                table: "Teslimler",
                newName: "IX_Teslimler_OdevId");

            migrationBuilder.RenameIndex(
                name: "IX_Odev_OgrenciId",
                table: "Odevler",
                newName: "IX_Odevler_OgrenciId");

            migrationBuilder.RenameIndex(
                name: "IX_Odev_DersId",
                table: "Odevler",
                newName: "IX_Odevler_DersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teslimler",
                table: "Teslimler",
                column: "TeslimId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Odevler",
                table: "Odevler",
                column: "OdevId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dersler",
                table: "Dersler",
                column: "DersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Odevler_AspNetUsers_OgrenciId",
                table: "Odevler",
                column: "OgrenciId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Odevler_Dersler_DersId",
                table: "Odevler",
                column: "DersId",
                principalTable: "Dersler",
                principalColumn: "DersId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teslimler_AspNetUsers_OgrenciId",
                table: "Teslimler",
                column: "OgrenciId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teslimler_Odevler_OdevId",
                table: "Teslimler",
                column: "OdevId",
                principalTable: "Odevler",
                principalColumn: "OdevId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
