using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DespViagem.Data.Migrations
{
    public partial class Inicial03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Viagens",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Descricao = table.Column<string>(type: "varchar(200)", nullable: true),
                    Cliente = table.Column<string>(type: "varchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viagens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ViagemId = table.Column<Guid>(nullable: false),
                    Logradouro = table.Column<string>(type: "varchar(200)", nullable: true),
                    Numero = table.Column<string>(type: "varchar(200)", nullable: true),
                    Complemento = table.Column<string>(type: "varchar(200)", nullable: true),
                    Cep = table.Column<string>(type: "varchar(200)", nullable: true),
                    Bairro = table.Column<string>(type: "varchar(200)", nullable: true),
                    Cidade = table.Column<string>(type: "varchar(200)", nullable: true),
                    Estado = table.Column<string>(type: "varchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enderecos_Viagens_ViagemId",
                        column: x => x.ViagemId,
                        principalTable: "Viagens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_ViagemId",
                table: "Enderecos",
                column: "ViagemId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropTable(
                name: "Viagens");
        }
    }
}
