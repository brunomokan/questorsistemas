using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuestorTeste.Migrations
{
    /// <inheritdoc />
    public partial class CreateBancosAndBoletosTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bancos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NomeDoBanco = table.Column<string>(type: "text", nullable: false),
                    CodigoDoBanco = table.Column<string>(type: "text", nullable: false),
                    PercentualDeJuros = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bancos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Boletos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NomeDoPagador = table.Column<string>(type: "text", nullable: false),
                    CpfCnpjDoPagador = table.Column<string>(type: "text", nullable: false),
                    NomeDoBeneficiario = table.Column<string>(type: "text", nullable: false),
                    CpfCnpjDoBeneficiario = table.Column<string>(type: "text", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false),
                    DataDeVencimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Observacao = table.Column<string>(type: "text", nullable: true),
                    BancoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boletos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bancos");

            migrationBuilder.DropTable(
                name: "Boletos");
        }
    }
}
