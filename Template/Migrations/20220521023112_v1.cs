using Microsoft.EntityFrameworkCore.Migrations;

namespace Template.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brend",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brend", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Prodavnica",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodavnica", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tip",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tip", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BrendProdavnica",
                columns: table => new
                {
                    BrendID = table.Column<int>(type: "int", nullable: false),
                    ProdavnicaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrendProdavnica", x => new { x.BrendID, x.ProdavnicaID });
                    table.ForeignKey(
                        name: "FK_BrendProdavnica_Brend_BrendID",
                        column: x => x.BrendID,
                        principalTable: "Brend",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrendProdavnica_Prodavnica_ProdavnicaID",
                        column: x => x.ProdavnicaID,
                        principalTable: "Prodavnica",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdavnicaTip",
                columns: table => new
                {
                    ProdavnicaID = table.Column<int>(type: "int", nullable: false),
                    TipID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdavnicaTip", x => new { x.ProdavnicaID, x.TipID });
                    table.ForeignKey(
                        name: "FK_ProdavnicaTip_Prodavnica_ProdavnicaID",
                        column: x => x.ProdavnicaID,
                        principalTable: "Prodavnica",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdavnicaTip_Tip_TipID",
                        column: x => x.TipID,
                        principalTable: "Tip",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proizvod",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sifra = table.Column<int>(type: "int", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    Cena = table.Column<int>(type: "int", nullable: false),
                    ProdavnicaID = table.Column<int>(type: "int", nullable: true),
                    BrendID = table.Column<int>(type: "int", nullable: true),
                    TipID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proizvod", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Proizvod_Brend_BrendID",
                        column: x => x.BrendID,
                        principalTable: "Brend",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Proizvod_Prodavnica_ProdavnicaID",
                        column: x => x.ProdavnicaID,
                        principalTable: "Prodavnica",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Proizvod_Tip_TipID",
                        column: x => x.TipID,
                        principalTable: "Tip",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BrendProdavnica_ProdavnicaID",
                table: "BrendProdavnica",
                column: "ProdavnicaID");

            migrationBuilder.CreateIndex(
                name: "IX_ProdavnicaTip_TipID",
                table: "ProdavnicaTip",
                column: "TipID");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvod_BrendID",
                table: "Proizvod",
                column: "BrendID");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvod_ProdavnicaID",
                table: "Proizvod",
                column: "ProdavnicaID");

            migrationBuilder.CreateIndex(
                name: "IX_Proizvod_TipID",
                table: "Proizvod",
                column: "TipID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrendProdavnica");

            migrationBuilder.DropTable(
                name: "ProdavnicaTip");

            migrationBuilder.DropTable(
                name: "Proizvod");

            migrationBuilder.DropTable(
                name: "Brend");

            migrationBuilder.DropTable(
                name: "Prodavnica");

            migrationBuilder.DropTable(
                name: "Tip");
        }
    }
}
