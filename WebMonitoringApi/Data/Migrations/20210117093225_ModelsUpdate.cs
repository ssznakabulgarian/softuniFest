using Microsoft.EntityFrameworkCore.Migrations;

namespace WebMonitoringApi.Data.Migrations
{
    public partial class ModelsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sent",
                table: "Logs",
                newName: "SentOn");

            migrationBuilder.RenameColumn(
                name: "Received",
                table: "Logs",
                newName: "ReceivedOn");

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "Urls",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Method",
                table: "Urls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Headers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogId = table.Column<int>(type: "int", nullable: true),
                    UrlId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Headers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Headers_Logs_LogId",
                        column: x => x.LogId,
                        principalTable: "Logs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Headers_Urls_UrlId",
                        column: x => x.UrlId,
                        principalTable: "Urls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Headers_LogId",
                table: "Headers",
                column: "LogId");

            migrationBuilder.CreateIndex(
                name: "IX_Headers_UrlId",
                table: "Headers",
                column: "UrlId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Headers");

            migrationBuilder.DropColumn(
                name: "Body",
                table: "Urls");

            migrationBuilder.DropColumn(
                name: "Method",
                table: "Urls");

            migrationBuilder.DropColumn(
                name: "Body",
                table: "Logs");

            migrationBuilder.RenameColumn(
                name: "SentOn",
                table: "Logs",
                newName: "Sent");

            migrationBuilder.RenameColumn(
                name: "ReceivedOn",
                table: "Logs",
                newName: "Received");
        }
    }
}
