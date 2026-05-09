using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class add_easy_order_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EasyOrderProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thumb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Images = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<int>(type: "int", nullable: true),
                    Hidden = table.Column<bool>(type: "bit", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    TrackStock = table.Column<bool>(type: "bit", nullable: true),
                    DisableOrdersForNoStock = table.Column<bool>(type: "bit", nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShowLandingInSamePage = table.Column<bool>(type: "bit", nullable: true),
                    IsSkipCart = table.Column<bool>(type: "bit", nullable: true),
                    BuyNowText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFixedBottomBuy = table.Column<bool>(type: "bit", nullable: true),
                    IsOnePageCheckout = table.Column<bool>(type: "bit", nullable: true),
                    IsFakeVisitors = table.Column<bool>(type: "bit", nullable: true),
                    FakeVisitorsMin = table.Column<int>(type: "int", nullable: true),
                    FakeVisitorsMax = table.Column<int>(type: "int", nullable: true),
                    IsFakeStock = table.Column<bool>(type: "bit", nullable: true),
                    IsFakeTimer = table.Column<bool>(type: "bit", nullable: true),
                    FakeTimerHours = table.Column<int>(type: "int", nullable: true),
                    IsQuantityHidden = table.Column<bool>(type: "bit", nullable: true),
                    IsHeaderHidden = table.Column<bool>(type: "bit", nullable: true),
                    IsFreeShipping = table.Column<bool>(type: "bit", nullable: true),
                    CustomCurrency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCheckoutBeforeDescription = table.Column<bool>(type: "bit", nullable: true),
                    HideRelatedProducts = table.Column<bool>(type: "bit", nullable: true),
                    IsTaagerSubmitActive = table.Column<bool>(type: "bit", nullable: true),
                    IsEcomboSubmitActive = table.Column<bool>(type: "bit", nullable: true),
                    IsMosaweqSubmitActive = table.Column<bool>(type: "bit", nullable: true),
                    IsAlturkySubmitActive = table.Column<bool>(type: "bit", nullable: true),
                    IsJamaicaSubmitActive = table.Column<bool>(type: "bit", nullable: true),
                    IsEngznySubmitActive = table.Column<bool>(type: "bit", nullable: true),
                    IsDigital = table.Column<bool>(type: "bit", nullable: true),
                    IsCloakingActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyOrderProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EasyOrderRequests",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    store_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cost = table.Column<int>(type: "int", nullable: true),
                    shipping_cost = table.Column<int>(type: "int", nullable: true),
                    total_cost = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    short_id = table.Column<int>(type: "int", nullable: true),
                    full_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    government = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    payment_method = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    utm_source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    utm_campaign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ip_country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyOrderRequests", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "EasyOrderCartItems",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    variant_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    store_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    price = table.Column<int>(type: "int", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    productId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    order_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EasyOrderRequestid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyOrderCartItems", x => x.id);
                    table.ForeignKey(
                        name: "FK_EasyOrderCartItems_EasyOrderProducts_productId",
                        column: x => x.productId,
                        principalTable: "EasyOrderProducts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EasyOrderCartItems_EasyOrderRequests_EasyOrderRequestid",
                        column: x => x.EasyOrderRequestid,
                        principalTable: "EasyOrderRequests",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EasyOrderCartItems_EasyOrderRequestid",
                table: "EasyOrderCartItems",
                column: "EasyOrderRequestid");

            migrationBuilder.CreateIndex(
                name: "IX_EasyOrderCartItems_productId",
                table: "EasyOrderCartItems",
                column: "productId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EasyOrderCartItems");

            migrationBuilder.DropTable(
                name: "EasyOrderProducts");

            migrationBuilder.DropTable(
                name: "EasyOrderRequests");
        }
    }
}
