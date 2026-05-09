using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class easy_order_columns2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Thumb",
                table: "EasyOrderProducts",
                newName: "thumb");

            migrationBuilder.RenameColumn(
                name: "Slug",
                table: "EasyOrderProducts",
                newName: "slug");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "EasyOrderProducts",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "Position",
                table: "EasyOrderProducts",
                newName: "position");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "EasyOrderProducts",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Images",
                table: "EasyOrderProducts",
                newName: "images");

            migrationBuilder.RenameColumn(
                name: "Hidden",
                table: "EasyOrderProducts",
                newName: "hidden");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "EasyOrderProducts",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "IsTaagerSubmitActive",
                table: "EasyOrderProducts",
                newName: "is_taager_submit_active");

            migrationBuilder.RenameColumn(
                name: "IsQuantityHidden",
                table: "EasyOrderProducts",
                newName: "is_quantity_hidden");

            migrationBuilder.RenameColumn(
                name: "IsMosaweqSubmitActive",
                table: "EasyOrderProducts",
                newName: "is_mosaweq_submit_active");

            migrationBuilder.RenameColumn(
                name: "IsJamaicaSubmitActive",
                table: "EasyOrderProducts",
                newName: "is_jamaica_submit_active");

            migrationBuilder.RenameColumn(
                name: "IsHeaderHidden",
                table: "EasyOrderProducts",
                newName: "is_header_hidden");

            migrationBuilder.RenameColumn(
                name: "IsFreeShipping",
                table: "EasyOrderProducts",
                newName: "is_free_shipping");

            migrationBuilder.RenameColumn(
                name: "IsEngznySubmitActive",
                table: "EasyOrderProducts",
                newName: "is_engzny_submit_active");

            migrationBuilder.RenameColumn(
                name: "IsEcomboSubmitActive",
                table: "EasyOrderProducts",
                newName: "is_ecombo_submit_active");

            migrationBuilder.RenameColumn(
                name: "IsAlturkySubmitActive",
                table: "EasyOrderProducts",
                newName: "is_alturky_submit_active");

            migrationBuilder.RenameColumn(
                name: "HideRelatedProducts",
                table: "EasyOrderProducts",
                newName: "hide_related_products");

            migrationBuilder.RenameColumn(
                name: "CustomCurrency",
                table: "EasyOrderProducts",
                newName: "custom_currency");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "thumb",
                table: "EasyOrderProducts",
                newName: "Thumb");

            migrationBuilder.RenameColumn(
                name: "slug",
                table: "EasyOrderProducts",
                newName: "Slug");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "EasyOrderProducts",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "position",
                table: "EasyOrderProducts",
                newName: "Position");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "EasyOrderProducts",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "images",
                table: "EasyOrderProducts",
                newName: "Images");

            migrationBuilder.RenameColumn(
                name: "hidden",
                table: "EasyOrderProducts",
                newName: "Hidden");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "EasyOrderProducts",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "is_taager_submit_active",
                table: "EasyOrderProducts",
                newName: "IsTaagerSubmitActive");

            migrationBuilder.RenameColumn(
                name: "is_quantity_hidden",
                table: "EasyOrderProducts",
                newName: "IsQuantityHidden");

            migrationBuilder.RenameColumn(
                name: "is_mosaweq_submit_active",
                table: "EasyOrderProducts",
                newName: "IsMosaweqSubmitActive");

            migrationBuilder.RenameColumn(
                name: "is_jamaica_submit_active",
                table: "EasyOrderProducts",
                newName: "IsJamaicaSubmitActive");

            migrationBuilder.RenameColumn(
                name: "is_header_hidden",
                table: "EasyOrderProducts",
                newName: "IsHeaderHidden");

            migrationBuilder.RenameColumn(
                name: "is_free_shipping",
                table: "EasyOrderProducts",
                newName: "IsFreeShipping");

            migrationBuilder.RenameColumn(
                name: "is_engzny_submit_active",
                table: "EasyOrderProducts",
                newName: "IsEngznySubmitActive");

            migrationBuilder.RenameColumn(
                name: "is_ecombo_submit_active",
                table: "EasyOrderProducts",
                newName: "IsEcomboSubmitActive");

            migrationBuilder.RenameColumn(
                name: "is_alturky_submit_active",
                table: "EasyOrderProducts",
                newName: "IsAlturkySubmitActive");

            migrationBuilder.RenameColumn(
                name: "hide_related_products",
                table: "EasyOrderProducts",
                newName: "HideRelatedProducts");

            migrationBuilder.RenameColumn(
                name: "custom_currency",
                table: "EasyOrderProducts",
                newName: "CustomCurrency");
        }
    }
}
