using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FuelServices.DBContext.Migrations
{
    public partial class tt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupplierPackagesLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    FuelSupplierId = table.Column<int>(nullable: true),
                    PaymentPackageId = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Company = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: false),
                    CityId = table.Column<int>(nullable: false),
                    PostalCode = table.Column<string>(nullable: false),
                    Address1 = table.Column<string>(nullable: false),
                    Address2 = table.Column<string>(nullable: true),
                    Phone1 = table.Column<string>(nullable: false),
                    Phone2 = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierPackagesLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierPackagesLog_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplierPackagesLog_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FuelSupplierPackagesLog_Customer",
                        column: x => x.FuelSupplierId,
                        principalTable: "FuelSupplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_FuelSupplierPackagesLog_PaymentPackage",
                        column: x => x.PaymentPackageId,
                        principalTable: "PaymentPackage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPackagesLog_CityId",
                table: "SupplierPackagesLog",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPackagesLog_CountryId",
                table: "SupplierPackagesLog",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPackagesLog_FuelSupplierId",
                table: "SupplierPackagesLog",
                column: "FuelSupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPackagesLog_PaymentPackageId",
                table: "SupplierPackagesLog",
                column: "PaymentPackageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupplierPackagesLog");
        }
    }
}
