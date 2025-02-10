using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataProcessorService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHttpLogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "RequestLogs",
                newName: "HttpLogs");

            migrationBuilder.RenameColumn(
                name: "RequestTime",
                table: "HttpLogs",
                newName: "Time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Time",
                table: "HttpLogs",
                newName: "RequestTime");

            migrationBuilder.RenameTable(
                name: "HttpLogs",
                newName: "RequestLogs");
        }
    }
}
