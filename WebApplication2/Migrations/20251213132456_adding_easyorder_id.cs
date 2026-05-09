using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class adding_easyorder_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EasyOrderRequestId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EasyOrderRequestId",
                table: "Orders",
                column: "EasyOrderRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_EasyOrderRequests_EasyOrderRequestId",
                table: "Orders",
                column: "EasyOrderRequestId",
                principalTable: "EasyOrderRequests",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_EasyOrderRequests_EasyOrderRequestId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_EasyOrderRequestId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "EasyOrderRequestId",
                table: "Orders");
        }
    }
}
