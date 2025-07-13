using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAS.ScrapingManagementService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDataSourceTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DataSourceTypeId",
                table: "DataSources",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "DataSourceTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSourceTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataSources_DataSourceTypeId",
                table: "DataSources",
                column: "DataSourceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DataSources_DataSourceTypes_DataSourceTypeId",
                table: "DataSources",
                column: "DataSourceTypeId",
                principalTable: "DataSourceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataSources_DataSourceTypes_DataSourceTypeId",
                table: "DataSources");

            migrationBuilder.DropTable(
                name: "DataSourceTypes");

            migrationBuilder.DropIndex(
                name: "IX_DataSources_DataSourceTypeId",
                table: "DataSources");

            migrationBuilder.DropColumn(
                name: "DataSourceTypeId",
                table: "DataSources");
        }
    }
}
