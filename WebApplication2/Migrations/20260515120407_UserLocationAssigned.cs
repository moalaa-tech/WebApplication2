using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class UserLocationAssigned : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsedAssignedLocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    StatesId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsedAssignedLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsedAssignedLocations_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsedAssignedLocations_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsedAssignedLocations_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsedAssignedLocations_States_StatesId",
                        column: x => x.StatesId,
                        principalTable: "States",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsedAssignedLocation_EmployeeId",
                table: "UsedAssignedLocations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_UsedAssignedLocation_Location",
                table: "UsedAssignedLocations",
                columns: new[] { "CountryId", "StatesId", "CityId" });

            migrationBuilder.CreateIndex(
                name: "IX_UsedAssignedLocations_CityId",
                table: "UsedAssignedLocations",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_UsedAssignedLocations_StatesId",
                table: "UsedAssignedLocations",
                column: "StatesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsedAssignedLocations");
        }
    }
}
