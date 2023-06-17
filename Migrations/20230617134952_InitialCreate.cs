using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RezerwacjaBoiska.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boiska",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nazwa = table.Column<string>(type: "TEXT", nullable: false),
                    Lokalizacja = table.Column<string>(type: "TEXT", nullable: false),
                    Rozmiar = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boiska", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gracz",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Imie = table.Column<string>(type: "TEXT", nullable: false),
                    Nazwisko = table.Column<string>(type: "TEXT", nullable: false),
                    Adres = table.Column<string>(type: "TEXT", nullable: false),
                    NumerTelefonu = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gracz", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Opinie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Ocena = table.Column<int>(type: "INTEGER", nullable: false),
                    Komentarz = table.Column<string>(type: "TEXT", nullable: false),
                    DataDodania = table.Column<string>(type: "TEXT", nullable: false),
                    GraczeId = table.Column<int>(type: "INTEGER", nullable: true),
                    BoiskaId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opinie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Opinie_Boiska_BoiskaId",
                        column: x => x.BoiskaId,
                        principalTable: "Boiska",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Opinie_Gracz_GraczeId",
                        column: x => x.GraczeId,
                        principalTable: "Gracz",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rezerwacje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataRezerwacji = table.Column<DateTime>(type: "TEXT", nullable: false),
                    GodzinaRozpoczecia = table.Column<DateTime>(type: "TEXT", nullable: false),
                    GodzinaZakonczenia = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    GraczeId = table.Column<int>(type: "INTEGER", nullable: true),
                    BoiskaId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezerwacje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rezerwacje_Boiska_BoiskaId",
                        column: x => x.BoiskaId,
                        principalTable: "Boiska",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rezerwacje_Gracz_GraczeId",
                        column: x => x.GraczeId,
                        principalTable: "Gracz",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Opinie_BoiskaId",
                table: "Opinie",
                column: "BoiskaId");

            migrationBuilder.CreateIndex(
                name: "IX_Opinie_GraczeId",
                table: "Opinie",
                column: "GraczeId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezerwacje_BoiskaId",
                table: "Rezerwacje",
                column: "BoiskaId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezerwacje_GraczeId",
                table: "Rezerwacje",
                column: "GraczeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Opinie");

            migrationBuilder.DropTable(
                name: "Rezerwacje");

            migrationBuilder.DropTable(
                name: "Boiska");

            migrationBuilder.DropTable(
                name: "Gracz");
        }
    }
}
