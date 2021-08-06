using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DespViagem.Data.Migrations.Viagem
{
    public partial class ViagemStart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PerfilUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "varchar(200)", nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilUsuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    Documento = table.Column<string>(type: "varchar(20)", nullable: false),
                    Idade = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tela",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "varchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tela", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Viagem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "varchar(100)", nullable: false),
                    Cliente = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viagem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DVUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(200)", nullable: true),
                    Email = table.Column<string>(type: "varchar(200)", nullable: true),
                    UserName = table.Column<string>(type: "varchar(200)", nullable: true),
                    IdentityRerefenceId = table.Column<Guid>(nullable: false),
                    PerfilUsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DVUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DVUser_PerfilUsuario_PerfilUsuarioId",
                        column: x => x.PerfilUsuarioId,
                        principalTable: "PerfilUsuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelaFuncaoPerfilUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "varchar(200)", nullable: true),
                    DescriptionEN = table.Column<string>(type: "varchar(200)", nullable: true),
                    UserPerfilId = table.Column<int>(nullable: false),
                    IdScreen = table.Column<int>(nullable: false),
                    Insert = table.Column<bool>(nullable: false),
                    Update = table.Column<bool>(nullable: false),
                    Delete = table.Column<bool>(nullable: false),
                    View = table.Column<bool>(nullable: false),
                    List = table.Column<bool>(nullable: false),
                    Import = table.Column<bool>(nullable: false),
                    Export = table.Column<bool>(nullable: false),
                    PerfilUsuarioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelaFuncaoPerfilUsuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TelaFuncaoPerfilUsuario_PerfilUsuario_PerfilUsuarioId",
                        column: x => x.PerfilUsuarioId,
                        principalTable: "PerfilUsuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Despesa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViagemId = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(type: "varchar(200)", nullable: false),
                    Local = table.Column<string>(type: "varchar(200)", nullable: false),
                    DataDespesa = table.Column<DateTime>(nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Observacao = table.Column<string>(type: "varchar(1000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Despesa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Despesa_Viagem_ViagemId",
                        column: x => x.ViagemId,
                        principalTable: "Viagem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Logradouro = table.Column<string>(type: "varchar(200)", nullable: false),
                    Numero = table.Column<string>(type: "varchar(20)", nullable: true),
                    Complemento = table.Column<string>(type: "varchar(500)", nullable: true),
                    Cep = table.Column<string>(type: "varchar(10)", nullable: false),
                    Bairro = table.Column<string>(type: "varchar(100)", nullable: false),
                    Cidade = table.Column<string>(type: "varchar(100)", nullable: false),
                    Estado = table.Column<string>(type: "varchar(50)", nullable: false),
                    ViagemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enderecos_Viagem_ViagemId",
                        column: x => x.ViagemId,
                        principalTable: "Viagem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Despesa_ViagemId",
                table: "Despesa",
                column: "ViagemId");

            migrationBuilder.CreateIndex(
                name: "IX_DVUser_PerfilUsuarioId",
                table: "DVUser",
                column: "PerfilUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_ViagemId",
                table: "Enderecos",
                column: "ViagemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TelaFuncaoPerfilUsuario_PerfilUsuarioId",
                table: "TelaFuncaoPerfilUsuario",
                column: "PerfilUsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Despesa");

            migrationBuilder.DropTable(
                name: "DVUser");

            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropTable(
                name: "Pessoas");

            migrationBuilder.DropTable(
                name: "Tela");

            migrationBuilder.DropTable(
                name: "TelaFuncaoPerfilUsuario");

            migrationBuilder.DropTable(
                name: "Viagem");

            migrationBuilder.DropTable(
                name: "PerfilUsuario");
        }
    }
}
