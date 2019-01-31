using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace Alten.CarTracker.Services.StatusReceivedService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    pkCarId = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.pkCarId);
                });

            migrationBuilder.CreateTable(
                name: "CarStatusesLookup",
                columns: table => new
                {
                    pkCarStatusLookupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarStatusesLookup", x => x.pkCarStatusLookupId);
                });

            migrationBuilder.CreateTable(
                name: "CarStatuses",
                columns: table => new
                {
                    pkStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReceivedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Location = table.Column<Point>(nullable: false),
                    CarId = table.Column<string>(nullable: false),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarStatuses", x => x.pkStatusId);
                    table.ForeignKey(
                        name: "FK_CarStatuses_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "pkCarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarStatuses_CarStatusesLookup_StatusId",
                        column: x => x.StatusId,
                        principalTable: "CarStatusesLookup",
                        principalColumn: "pkCarStatusLookupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CarStatusesLookup",
                columns: new[] { "pkCarStatusLookupId", "Name" },
                values: new object[,]
                {
                    { 1, "Stopped" },
                    { 2, "Moving" },
                    { 3, "Disconnected" }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                column: "pkCarId",
                values: new object[]
                {
                    "YS2R4X20005399401",
                    "VLUR4X20009093588",
                    "VLUR4X20009048044",
                    "YS2R4X20005388011",
                    "YS2R4X20005387949",
                    "VLUR4X20009048066",
                    "YS2R4X20005387055"
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarStatuses_CarId",
                table: "CarStatuses",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_CarStatuses_StatusId",
                table: "CarStatuses",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarStatuses");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "CarStatusesLookup");
        }
    }
}
