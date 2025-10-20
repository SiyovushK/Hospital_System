using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BagRemove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DischargeDate",
                table: "Patients",
                newName: "RecoveryDate");

            migrationBuilder.RenameColumn(
                name: "TerritoryCode",
                table: "Hospitals",
                newName: "TerritoryName");

            migrationBuilder.RenameColumn(
                name: "MinistryCode",
                table: "Hospitals",
                newName: "MinistryName");

            migrationBuilder.RenameColumn(
                name: "DistrictCode",
                table: "Hospitals",
                newName: "DistrictName");

            migrationBuilder.RenameColumn(
                name: "CityCode",
                table: "Hospitals",
                newName: "CityName");

            migrationBuilder.AddColumn<int>(
                name: "TerritoryName",
                table: "Patients",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TerritoryName",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "RecoveryDate",
                table: "Patients",
                newName: "DischargeDate");

            migrationBuilder.RenameColumn(
                name: "TerritoryName",
                table: "Hospitals",
                newName: "TerritoryCode");

            migrationBuilder.RenameColumn(
                name: "MinistryName",
                table: "Hospitals",
                newName: "MinistryCode");

            migrationBuilder.RenameColumn(
                name: "DistrictName",
                table: "Hospitals",
                newName: "DistrictCode");

            migrationBuilder.RenameColumn(
                name: "CityName",
                table: "Hospitals",
                newName: "CityCode");
        }
    }
}
