using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebServer.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SeloDouments",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeloDouments");
        }
    }
}
