using Microsoft.EntityFrameworkCore.Migrations;

namespace FuelServices.DBContext.Migrations
{
    public partial class Add_Adds2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisement_AdvertisementImageType_AdvertisementImageTypeId",
                table: "Advertisement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdvertisementImageType",
                table: "AdvertisementImageType");

            migrationBuilder.RenameTable(
                name: "AdvertisementImageType",
                newName: "AdvertisementImageTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdvertisementImageTypes",
                table: "AdvertisementImageTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisement_AdvertisementImageTypes_AdvertisementImageTypeId",
                table: "Advertisement",
                column: "AdvertisementImageTypeId",
                principalTable: "AdvertisementImageTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisement_AdvertisementImageTypes_AdvertisementImageTypeId",
                table: "Advertisement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdvertisementImageTypes",
                table: "AdvertisementImageTypes");

            migrationBuilder.RenameTable(
                name: "AdvertisementImageTypes",
                newName: "AdvertisementImageType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdvertisementImageType",
                table: "AdvertisementImageType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisement_AdvertisementImageType_AdvertisementImageTypeId",
                table: "Advertisement",
                column: "AdvertisementImageTypeId",
                principalTable: "AdvertisementImageType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
