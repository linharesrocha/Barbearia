using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Barbearia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addagendamento2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
