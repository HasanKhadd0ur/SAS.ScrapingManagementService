using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAS.ScrapingManagementService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixScrapingTasksRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataSources_ScrapingTasks_ScrapingTaskId",
                table: "DataSources");

            migrationBuilder.DropIndex(
                name: "IX_DataSources_ScrapingTaskId",
                table: "DataSources");

            migrationBuilder.DropColumn(
                name: "ScrapingTaskId",
                table: "DataSources");

            migrationBuilder.CreateTable(
                name: "ScrapingTaskDataSources",
                columns: table => new
                {
                    ScrapingTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataSourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScrapingTaskDataSources", x => new { x.ScrapingTaskId, x.DataSourceId });
                    table.ForeignKey(
                        name: "FK_ScrapingTaskDataSources_DataSources_DataSourceId",
                        column: x => x.DataSourceId,
                        principalTable: "DataSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScrapingTaskDataSources_ScrapingTasks_ScrapingTaskId",
                        column: x => x.ScrapingTaskId,
                        principalTable: "ScrapingTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScrapingTaskDataSources_DataSourceId",
                table: "ScrapingTaskDataSources",
                column: "DataSourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScrapingTaskDataSources");

            migrationBuilder.AddColumn<Guid>(
                name: "ScrapingTaskId",
                table: "DataSources",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DataSources_ScrapingTaskId",
                table: "DataSources",
                column: "ScrapingTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_DataSources_ScrapingTasks_ScrapingTaskId",
                table: "DataSources",
                column: "ScrapingTaskId",
                principalTable: "ScrapingTasks",
                principalColumn: "Id");
        }
    }
}
