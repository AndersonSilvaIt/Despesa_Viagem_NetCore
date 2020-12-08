using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DespViagem.Data.Migrations
{
    public partial class Inicial04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Despesas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ViagemId = table.Column<Guid>(nullable: false),
                    Descricao = table.Column<string>(type: "varchar(200)", nullable: true),
                    Local = table.Column<string>(type: "varchar(200)", nullable: true),
                    DataDespesa = table.Column<DateTime>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    Observacao = table.Column<string>(type: "varchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Despesas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Despesas_Viagens_ViagemId",
                        column: x => x.ViagemId,
                        principalTable: "Viagens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_ViagemId",
                table: "Despesas",
                column: "ViagemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Despesas");
        }
    }
}
