using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barbearia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addagendamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agendamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    DataHorario = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ObservacaoCliente = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ObservacaoBarbeiro = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DataSolicitacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    DataAprovacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataCancelamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agendamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agendamento_Usuarios_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AgendamentosServicos",
                columns: table => new
                {
                    AgendamentoId = table.Column<int>(type: "int", nullable: false),
                    ServicoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendamentosServicos", x => new { x.AgendamentoId, x.ServicoId });
                    table.ForeignKey(
                        name: "FK_AgendamentosServicos_Agendamento_AgendamentoId",
                        column: x => x.AgendamentoId,
                        principalTable: "Agendamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgendamentosServicos_Servicos_ServicoId",
                        column: x => x.ServicoId,
                        principalTable: "Servicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Perfis",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "5c951181-98b5-412f-9153-685157a6f505");

            migrationBuilder.UpdateData(
                table: "Perfis",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "704a468b-7c71-4640-a67d-0c73fa821474");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_ClienteId",
                table: "Agendamento",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_DataHorario",
                table: "Agendamento",
                column: "DataHorario");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_Status",
                table: "Agendamento",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_AgendamentosServicos_ServicoId",
                table: "AgendamentosServicos",
                column: "ServicoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgendamentosServicos");

            migrationBuilder.DropTable(
                name: "Agendamento");

            migrationBuilder.UpdateData(
                table: "Perfis",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "333a2610-bf6f-4328-a6c3-99cfc33e113d");

            migrationBuilder.UpdateData(
                table: "Perfis",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "cd16aed9-71ca-4434-8d9b-d16f3f12736b");
        }
    }
}
