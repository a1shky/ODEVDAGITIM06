using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ODEVDAGITIM06.Migrations
{
    /// <inheritdoc />
    public partial class IlkKurulum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dersler",
                columns: table => new
                {
                    DersId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DersAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DersKodu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DersAciklamasi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dersler", x => x.DersId);
                });

            migrationBuilder.CreateTable(
                name: "Odevler",
                columns: table => new
                {
                    OdevId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OdevBasligi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OlusturmaTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TeslimTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odevler", x => x.OdevId);
                    table.ForeignKey(
                        name: "FK_Odevler_Dersler_DersId",
                        column: x => x.DersId,
                        principalTable: "Dersler",
                        principalColumn: "DersId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teslimler",
                columns: table => new
                {
                    TeslimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeslimTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DosyaYolu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Not = table.Column<int>(type: "int", nullable: true),
                    OdevId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teslimler", x => x.TeslimId);
                    table.ForeignKey(
                        name: "FK_Teslimler_Odevler_OdevId",
                        column: x => x.OdevId,
                        principalTable: "Odevler",
                        principalColumn: "OdevId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Odevler_DersId",
                table: "Odevler",
                column: "DersId");

            migrationBuilder.CreateIndex(
                name: "IX_Teslimler_OdevId",
                table: "Teslimler",
                column: "OdevId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Teslimler");

            migrationBuilder.DropTable(
                name: "Odevler");

            migrationBuilder.DropTable(
                name: "Dersler");
        }
    }
}
