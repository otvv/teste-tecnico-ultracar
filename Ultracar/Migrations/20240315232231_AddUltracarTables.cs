using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Ultracar.Context;

#nullable disable

namespace Ultracar.Migrations
{
    /// <inheritdoc />
    public partial class AddUltracarTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orcamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NumeracaoOrcamento = table.Column<string>(type: "text", nullable: false),
                    PlacaVeiculo = table.Column<string>(type: "text", nullable: false),
                    NomeCliente = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orcamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pecas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NomePeca = table.Column<string>(type: "text", nullable: false),
                    EstoquePeca = table.Column<int>(type: "integer", nullable: false),
                    OrcamentoId = table.Column<int>(type: "integer", nullable: true),
                    Entregue = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pecas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pecas_Orcamentos_OrcamentoId",
                        column: x => x.OrcamentoId,
                        principalTable: "Orcamentos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Estoque",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PecaId = table.Column<int>(type: "integer", nullable: false),
                    DataMovimentacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Quantidade = table.Column<int>(type: "integer", nullable: false),
                    TipoMovimentacao = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estoque", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estoque_Pecas_PecaId",
                        column: x => x.PecaId,
                        principalTable: "Pecas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estoque_PecaId",
                table: "Estoque",
                column: "PecaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pecas_OrcamentoId",
                table: "Pecas",
                column: "OrcamentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estoque");

            migrationBuilder.DropTable(
                name: "Pecas");

            migrationBuilder.DropTable(
                name: "Orcamentos");
        }
    }
}
