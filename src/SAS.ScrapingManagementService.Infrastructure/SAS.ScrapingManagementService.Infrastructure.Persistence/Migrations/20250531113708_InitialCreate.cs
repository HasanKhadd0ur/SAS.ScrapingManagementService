using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SAS.ScrapingManagementService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Platforms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platforms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Scrapers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScraperName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Hostname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RegisteredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TasksHandled = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scrapers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScrapingDomains",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NormalisedName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScrapingDomains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScrapingTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScraperId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DomainId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScrapingTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScrapingTasks_Scrapers_ScraperId",
                        column: x => x.ScraperId,
                        principalTable: "Scrapers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScrapingTasks_ScrapingDomains_DomainId",
                        column: x => x.DomainId,
                        principalTable: "ScrapingDomains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DataSources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Traget = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PlatformId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DomainId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScrapingTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataSources_Platforms_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DataSources_ScrapingDomains_DomainId",
                        column: x => x.DomainId,
                        principalTable: "ScrapingDomains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DataSources_ScrapingTasks_ScrapingTaskId",
                        column: x => x.ScrapingTaskId,
                        principalTable: "ScrapingTasks",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Platforms",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("6835f670-2e5c-8000-b5bb-ea9b1705cedb"), "Twitter/X social media platform", "Twitter" },
                    { new Guid("6835f670-2e5c-8000-b5bb-ea9b1705cede"), "Telegram social media platform", "Telegram" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataSources_DomainId",
                table: "DataSources",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_DataSources_PlatformId",
                table: "DataSources",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_DataSources_ScrapingTaskId",
                table: "DataSources",
                column: "ScrapingTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ScrapingTasks_DomainId",
                table: "ScrapingTasks",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_ScrapingTasks_ScraperId",
                table: "ScrapingTasks",
                column: "ScraperId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataSources");

            migrationBuilder.DropTable(
                name: "Platforms");

            migrationBuilder.DropTable(
                name: "ScrapingTasks");

            migrationBuilder.DropTable(
                name: "Scrapers");

            migrationBuilder.DropTable(
                name: "ScrapingDomains");
        }
    }
}
