using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Alten.CarTracker.BackEndApi.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Customers",
                columns: table => new
                {
                    pkCustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Address = table.Column<string>(maxLength: 350, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.pkCustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    pkCarId = table.Column<string>(maxLength: 25, nullable: false),
                    RegistrationNumber = table.Column<string>(maxLength: 10, nullable: false),
                    fkCustomerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.pkCarId);
                    table.ForeignKey(
                        name: "FK_Cars_Customers_fkCustomerId",
                        column: x => x.fkCustomerId,
                        principalTable: "Customers",
                        principalColumn: "pkCustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CarStatusesLookup",
                columns: new[] { "pkCarStatusLookupId", "Name" },
                values: new object[,]
                {
                    { 1, "Stopped" },
                    { 2, "Moving" },
                    { 3, "Connected" },
                    { 4, "Disconnected" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "pkCustomerId", "Address", "Name" },
                values: new object[,]
                {
                    { 1, "Cementvägen 8, 111 11 Södertälje", "Kalles Grustransporter AB" },
                    { 2, "Balkvägen 12, 222 22 Stockholm", "Johans Bulk AB" },
                    { 3, "Budgetvägen 1, 333 33 Uppsala", "Haralds Värdetransporter AB" }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "pkCarId", "fkCustomerId", "RegistrationNumber" },
                values: new object[,]
                {
                    { "YS2R4X20005399401", 1, "ABC123" },
                    { "VLUR4X20009093588", 1, "DEF456" },
                    { "VLUR4X20009048044", 1, "GHI789" },
                    { "YS2R4X20005388011", 2, "JKL012" },
                    { "YS2R4X20005387949", 2, "MNO345" },
                    { "VLUR4X20009048066", 3, "PQR678" },
                    { "YS2R4X20005387055", 3, "STU901" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_fkCustomerId",
                table: "Cars",
                column: "fkCustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "CarStatusesLookup");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
