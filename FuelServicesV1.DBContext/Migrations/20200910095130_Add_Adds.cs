using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FuelServices.DBContext.Migrations
{
    public partial class Add_Adds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "AdvertisementType");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Advertisement");

            migrationBuilder.AddColumn<int>(
                name: "AdvertisementTypeId",
                table: "AdvertisementTypeProperty",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "AdvertisementTypeProperty",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ExceptAdvertisementType1Id",
                table: "AdvertisementTypeProperty",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExceptAdvertisementType2Id",
                table: "AdvertisementTypeProperty",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Options",
                table: "AdvertisementTypeProperty",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "AdvertisementTypeProperty",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "AdvertisementType",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "AdvertisementTypePropertyId",
                table: "AdvertisementProperty",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AdvertisementId",
                table: "AdvertisementProperty",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "AdvertisementOwner",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "AdvertisementOwner",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserSpecializationId",
                table: "AdvertisementOwner",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Advertisement",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CaptionClicks",
                table: "Advertisement",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AnchorUrl",
                table: "Advertisement",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AdvertisementOwnerId",
                table: "Advertisement",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AdvertisementCategoryId",
                table: "Advertisement",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdvertisementImageTypeId",
                table: "Advertisement",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Advertisement",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "Advertisement",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "AdvertisementImageType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertisementImageType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AirportAds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    AdvertisementId = table.Column<int>(nullable: false),
                    AirportId = table.Column<int>(nullable: false),
                    Range = table.Column<int>(nullable: false),
                    CaptionClicks = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirportAds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AirportAds_Advertisement_AdvertisementId",
                        column: x => x.AdvertisementId,
                        principalTable: "Advertisement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AirportAds_Airport_AirportId",
                        column: x => x.AirportId,
                        principalTable: "Airport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementTypeProperty_AdvertisementTypeId",
                table: "AdvertisementTypeProperty",
                column: "AdvertisementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_AdvertisementImageTypeId",
                table: "Advertisement",
                column: "AdvertisementImageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AirportAds_AdvertisementId",
                table: "AirportAds",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_AirportAds_AirportId",
                table: "AirportAds",
                column: "AirportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisement_AdvertisementImageType_AdvertisementImageTypeId",
                table: "Advertisement",
                column: "AdvertisementImageTypeId",
                principalTable: "AdvertisementImageType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertisementTypeProperty_AdvertisementType_AdvertisementTypeId",
                table: "AdvertisementTypeProperty",
                column: "AdvertisementTypeId",
                principalTable: "AdvertisementType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisement_AdvertisementImageType_AdvertisementImageTypeId",
                table: "Advertisement");

            migrationBuilder.DropForeignKey(
                name: "FK_AdvertisementTypeProperty_AdvertisementType_AdvertisementTypeId",
                table: "AdvertisementTypeProperty");

            migrationBuilder.DropTable(
                name: "AdvertisementImageType");

            migrationBuilder.DropTable(
                name: "AirportAds");

            migrationBuilder.DropIndex(
                name: "IX_AdvertisementTypeProperty_AdvertisementTypeId",
                table: "AdvertisementTypeProperty");

            migrationBuilder.DropIndex(
                name: "IX_Advertisement_AdvertisementImageTypeId",
                table: "Advertisement");

            migrationBuilder.DropColumn(
                name: "AdvertisementTypeId",
                table: "AdvertisementTypeProperty");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "AdvertisementTypeProperty");

            migrationBuilder.DropColumn(
                name: "ExceptAdvertisementType1Id",
                table: "AdvertisementTypeProperty");

            migrationBuilder.DropColumn(
                name: "ExceptAdvertisementType2Id",
                table: "AdvertisementTypeProperty");

            migrationBuilder.DropColumn(
                name: "Options",
                table: "AdvertisementTypeProperty");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "AdvertisementTypeProperty");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "AdvertisementType");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AdvertisementOwner");

            migrationBuilder.DropColumn(
                name: "UserSpecializationId",
                table: "AdvertisementOwner");

            migrationBuilder.DropColumn(
                name: "AdvertisementImageTypeId",
                table: "Advertisement");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Advertisement");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Advertisement");

            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "AdvertisementType",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AdvertisementTypePropertyId",
                table: "AdvertisementProperty",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AdvertisementId",
                table: "AdvertisementProperty",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "AdvertisementOwner",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Advertisement",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "CaptionClicks",
                table: "Advertisement",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AnchorUrl",
                table: "Advertisement",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "AdvertisementOwnerId",
                table: "Advertisement",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "AdvertisementCategoryId",
                table: "Advertisement",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Advertisement",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
