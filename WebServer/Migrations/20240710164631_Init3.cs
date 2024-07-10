using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebServer.Migrations
{
    /// <inheritdoc />
    public partial class Init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeloDouments");

            migrationBuilder.CreateTable(
                name: "SeloDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SeloFormId = table.Column<Guid>(type: "uuid", nullable: true),
                    KodNaselPunk = table.Column<string>(type: "text", nullable: false),
                    NameNaselPunk = table.Column<string>(type: "text", nullable: false),
                    KodOblast = table.Column<string>(type: "text", nullable: true),
                    KodRaiona = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    Year = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeloDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeloDocuments_SeloForms_SeloFormId",
                        column: x => x.SeloFormId,
                        principalTable: "SeloForms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeloDocuments_SeloFormId",
                table: "SeloDocuments",
                column: "SeloFormId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeloDocuments");

            migrationBuilder.CreateTable(
                name: "SeloDouments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SeloFormId = table.Column<Guid>(type: "uuid", nullable: true),
                    KodNaselPunk = table.Column<string>(type: "text", nullable: false),
                    KodOblast = table.Column<string>(type: "text", nullable: true),
                    KodRaiona = table.Column<string>(type: "text", nullable: true),
                    NameNaselPunk = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    Year = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeloDouments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeloDouments_SeloForms_SeloFormId",
                        column: x => x.SeloFormId,
                        principalTable: "SeloForms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeloDouments_SeloFormId",
                table: "SeloDouments",
                column: "SeloFormId");
        }
    }
}
