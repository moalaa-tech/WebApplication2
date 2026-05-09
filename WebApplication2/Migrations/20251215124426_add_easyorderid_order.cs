using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class add_easyorderid_order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EasyOrderProductId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_EasyOrderProductId",
                table: "Products",
                column: "EasyOrderProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_EasyOrderProducts_EasyOrderProductId",
                table: "Products",
                column: "EasyOrderProductId",
                principalTable: "EasyOrderProducts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_EasyOrderProducts_EasyOrderProductId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_EasyOrderProductId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "EasyOrderProductId",
                table: "Products");
        }
    }
}
