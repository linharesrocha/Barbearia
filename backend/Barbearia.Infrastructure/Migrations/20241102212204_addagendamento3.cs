using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barbearia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addagendamento3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamento_Usuarios_ClienteId",
                table: "Agendamento");

            migrationBuilder.DropForeignKey(
                name: "FK_AgendamentosServicos_Agendamento_AgendamentoId",
                table: "AgendamentosServicos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agendamento",
                table: "Agendamento");

            migrationBuilder.RenameTable(
                name: "Agendamento",
                newName: "Agendamentos");

            migrationBuilder.RenameIndex(
                name: "IX_Agendamento_Status",
                table: "Agendamentos",
                newName: "IX_Agendamentos_Status");

            migrationBuilder.RenameIndex(
                name: "IX_Agendamento_DataHorario",
                table: "Agendamentos",
                newName: "IX_Agendamentos_DataHorario");

            migrationBuilder.RenameIndex(
                name: "IX_Agendamento_ClienteId",
                table: "Agendamentos",
                newName: "IX_Agendamentos_ClienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agendamentos",
                table: "Agendamentos",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Perfis",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f85a2606-034a-4e83-93a6-f65757400e16");

            migrationBuilder.UpdateData(
                table: "Perfis",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "155e460f-c954-4935-87a0-85717da05602");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Usuarios_ClienteId",
                table: "Agendamentos",
                column: "ClienteId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AgendamentosServicos_Agendamentos_AgendamentoId",
                table: "AgendamentosServicos",
                column: "AgendamentoId",
                principalTable: "Agendamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Usuarios_ClienteId",
                table: "Agendamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_AgendamentosServicos_Agendamentos_AgendamentoId",
                table: "AgendamentosServicos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agendamentos",
                table: "Agendamentos");

            migrationBuilder.RenameTable(
                name: "Agendamentos",
                newName: "Agendamento");

            migrationBuilder.RenameIndex(
                name: "IX_Agendamentos_Status",
                table: "Agendamento",
                newName: "IX_Agendamento_Status");

            migrationBuilder.RenameIndex(
                name: "IX_Agendamentos_DataHorario",
                table: "Agendamento",
                newName: "IX_Agendamento_DataHorario");

            migrationBuilder.RenameIndex(
                name: "IX_Agendamentos_ClienteId",
                table: "Agendamento",
                newName: "IX_Agendamento_ClienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agendamento",
                table: "Agendamento",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Perfis",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "31ccaa65-8a82-4878-b1fd-6ff023f0e1ce");

            migrationBuilder.UpdateData(
                table: "Perfis",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "87b41603-7b31-4624-b562-ca3b748c88f6");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamento_Usuarios_ClienteId",
                table: "Agendamento",
                column: "ClienteId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AgendamentosServicos_Agendamento_AgendamentoId",
                table: "AgendamentosServicos",
                column: "AgendamentoId",
                principalTable: "Agendamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
