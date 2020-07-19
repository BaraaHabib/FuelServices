using Microsoft.EntityFrameworkCore.Migrations;

namespace FuelServices.DBContext.Migrations
{
    public partial class SupplierConfirmDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerConfirmDate",
                table: "RequestOffers",
                newName: "SupplierConfirmDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SupplierConfirmDate",
                table: "RequestOffers",
                newName: "CustomerConfirmDate");
        }
    }
}
