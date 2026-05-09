using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class easy_order_columns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "EasyOrderProducts",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "TrackStock",
                table: "EasyOrderProducts",
                newName: "track_stock");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "EasyOrderProducts",
                newName: "store_id");

            migrationBuilder.RenameColumn(
                name: "ShowLandingInSamePage",
                table: "EasyOrderProducts",
                newName: "show_landing_in_same_page");

            migrationBuilder.RenameColumn(
                name: "SalePrice",
                table: "EasyOrderProducts",
                newName: "sale_price");

            migrationBuilder.RenameColumn(
                name: "MetaDescription",
                table: "EasyOrderProducts",
                newName: "meta_description");

            migrationBuilder.RenameColumn(
                name: "IsSkipCart",
                table: "EasyOrderProducts",
                newName: "is_skip_cart");

            migrationBuilder.RenameColumn(
                name: "IsOnePageCheckout",
                table: "EasyOrderProducts",
                newName: "is_one_page_checkout");

            migrationBuilder.RenameColumn(
                name: "IsFixedBottomBuy",
                table: "EasyOrderProducts",
                newName: "is_fixed_bottom_buy");

            migrationBuilder.RenameColumn(
                name: "IsFakeVisitors",
                table: "EasyOrderProducts",
                newName: "is_fake_visitors");

            migrationBuilder.RenameColumn(
                name: "IsFakeTimer",
                table: "EasyOrderProducts",
                newName: "is_fake_timer");

            migrationBuilder.RenameColumn(
                name: "IsFakeStock",
                table: "EasyOrderProducts",
                newName: "is_fake_stock");

            migrationBuilder.RenameColumn(
                name: "IsDigital",
                table: "EasyOrderProducts",
                newName: "is_digital");

            migrationBuilder.RenameColumn(
                name: "IsCloakingActive",
                table: "EasyOrderProducts",
                newName: "is_cloaking_active");

            migrationBuilder.RenameColumn(
                name: "IsCheckoutBeforeDescription",
                table: "EasyOrderProducts",
                newName: "is_checkout_before_description");

            migrationBuilder.RenameColumn(
                name: "FakeVisitorsMin",
                table: "EasyOrderProducts",
                newName: "fake_visitors_min");

            migrationBuilder.RenameColumn(
                name: "FakeVisitorsMax",
                table: "EasyOrderProducts",
                newName: "fake_visitors_max");

            migrationBuilder.RenameColumn(
                name: "FakeTimerHours",
                table: "EasyOrderProducts",
                newName: "fake_timer_hours");

            migrationBuilder.RenameColumn(
                name: "DisableOrdersForNoStock",
                table: "EasyOrderProducts",
                newName: "disable_orders_for_no_stock");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "EasyOrderProducts",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "BuyNowText",
                table: "EasyOrderProducts",
                newName: "buy_now_text");

            migrationBuilder.AlterColumn<decimal>(
                name: "total_cost",
                table: "EasyOrderRequests",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "store_id",
                table: "EasyOrderRequests",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "shipping_cost",
                table: "EasyOrderRequests",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "cost",
                table: "EasyOrderRequests",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_day",
                table: "EasyOrderRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "note",
                table: "EasyOrderRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "variant_id",
                table: "EasyOrderCartItems",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "store_id",
                table: "EasyOrderCartItems",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "product_id",
                table: "EasyOrderCartItems",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "EasyOrderCartItems",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "order_id",
                table: "EasyOrderCartItems",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "EasyOrderCartItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "guest_id",
                table: "EasyOrderCartItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "in_cart",
                table: "EasyOrderCartItems",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_upsell",
                table: "EasyOrderCartItems",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "EasyOrderCartItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "variantid",
                table: "EasyOrderCartItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EasyOrderVariant",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    productId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    sale_price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    taager_code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyOrderVariant", x => x.id);
                    table.ForeignKey(
                        name: "FK_EasyOrderVariant_EasyOrderProducts_productId",
                        column: x => x.productId,
                        principalTable: "EasyOrderProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EasyOrderVariationProp",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    variation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    variation_prop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    product_variant_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    product_variantid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyOrderVariationProp", x => x.id);
                    table.ForeignKey(
                        name: "FK_EasyOrderVariationProp_EasyOrderVariant_product_variantid",
                        column: x => x.product_variantid,
                        principalTable: "EasyOrderVariant",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EasyOrderCartItems_variantid",
                table: "EasyOrderCartItems",
                column: "variantid");

            migrationBuilder.CreateIndex(
                name: "IX_EasyOrderVariant_productId",
                table: "EasyOrderVariant",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_EasyOrderVariationProp_product_variantid",
                table: "EasyOrderVariationProp",
                column: "product_variantid");

            migrationBuilder.AddForeignKey(
                name: "FK_EasyOrderCartItems_EasyOrderVariant_variantid",
                table: "EasyOrderCartItems",
                column: "variantid",
                principalTable: "EasyOrderVariant",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EasyOrderCartItems_EasyOrderVariant_variantid",
                table: "EasyOrderCartItems");

            migrationBuilder.DropTable(
                name: "EasyOrderVariationProp");

            migrationBuilder.DropTable(
                name: "EasyOrderVariant");

            migrationBuilder.DropIndex(
                name: "IX_EasyOrderCartItems_variantid",
                table: "EasyOrderCartItems");

            migrationBuilder.DropColumn(
                name: "created_day",
                table: "EasyOrderRequests");

            migrationBuilder.DropColumn(
                name: "note",
                table: "EasyOrderRequests");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "EasyOrderCartItems");

            migrationBuilder.DropColumn(
                name: "guest_id",
                table: "EasyOrderCartItems");

            migrationBuilder.DropColumn(
                name: "in_cart",
                table: "EasyOrderCartItems");

            migrationBuilder.DropColumn(
                name: "is_upsell",
                table: "EasyOrderCartItems");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "EasyOrderCartItems");

            migrationBuilder.DropColumn(
                name: "variantid",
                table: "EasyOrderCartItems");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "EasyOrderProducts",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "track_stock",
                table: "EasyOrderProducts",
                newName: "TrackStock");

            migrationBuilder.RenameColumn(
                name: "store_id",
                table: "EasyOrderProducts",
                newName: "StoreId");

            migrationBuilder.RenameColumn(
                name: "show_landing_in_same_page",
                table: "EasyOrderProducts",
                newName: "ShowLandingInSamePage");

            migrationBuilder.RenameColumn(
                name: "sale_price",
                table: "EasyOrderProducts",
                newName: "SalePrice");

            migrationBuilder.RenameColumn(
                name: "meta_description",
                table: "EasyOrderProducts",
                newName: "MetaDescription");

            migrationBuilder.RenameColumn(
                name: "is_skip_cart",
                table: "EasyOrderProducts",
                newName: "IsSkipCart");

            migrationBuilder.RenameColumn(
                name: "is_one_page_checkout",
                table: "EasyOrderProducts",
                newName: "IsOnePageCheckout");

            migrationBuilder.RenameColumn(
                name: "is_fixed_bottom_buy",
                table: "EasyOrderProducts",
                newName: "IsFixedBottomBuy");

            migrationBuilder.RenameColumn(
                name: "is_fake_visitors",
                table: "EasyOrderProducts",
                newName: "IsFakeVisitors");

            migrationBuilder.RenameColumn(
                name: "is_fake_timer",
                table: "EasyOrderProducts",
                newName: "IsFakeTimer");

            migrationBuilder.RenameColumn(
                name: "is_fake_stock",
                table: "EasyOrderProducts",
                newName: "IsFakeStock");

            migrationBuilder.RenameColumn(
                name: "is_digital",
                table: "EasyOrderProducts",
                newName: "IsDigital");

            migrationBuilder.RenameColumn(
                name: "is_cloaking_active",
                table: "EasyOrderProducts",
                newName: "IsCloakingActive");

            migrationBuilder.RenameColumn(
                name: "is_checkout_before_description",
                table: "EasyOrderProducts",
                newName: "IsCheckoutBeforeDescription");

            migrationBuilder.RenameColumn(
                name: "fake_visitors_min",
                table: "EasyOrderProducts",
                newName: "FakeVisitorsMin");

            migrationBuilder.RenameColumn(
                name: "fake_visitors_max",
                table: "EasyOrderProducts",
                newName: "FakeVisitorsMax");

            migrationBuilder.RenameColumn(
                name: "fake_timer_hours",
                table: "EasyOrderProducts",
                newName: "FakeTimerHours");

            migrationBuilder.RenameColumn(
                name: "disable_orders_for_no_stock",
                table: "EasyOrderProducts",
                newName: "DisableOrdersForNoStock");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "EasyOrderProducts",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "buy_now_text",
                table: "EasyOrderProducts",
                newName: "BuyNowText");

            migrationBuilder.AlterColumn<int>(
                name: "total_cost",
                table: "EasyOrderRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "store_id",
                table: "EasyOrderRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "shipping_cost",
                table: "EasyOrderRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "cost",
                table: "EasyOrderRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "variant_id",
                table: "EasyOrderCartItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "store_id",
                table: "EasyOrderCartItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "product_id",
                table: "EasyOrderCartItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "price",
                table: "EasyOrderCartItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "order_id",
                table: "EasyOrderCartItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
