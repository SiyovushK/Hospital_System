using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Hospitals_HospitalRegistrationNumber",
                table: "Patients");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Hospitals_HospitalRegistrationNumber",
                table: "Patients",
                column: "HospitalRegistrationNumber",
                principalTable: "Hospitals",
                principalColumn: "RegistrationNumber",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Hospitals_HospitalRegistrationNumber",
                table: "Patients");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Hospitals_HospitalRegistrationNumber",
                table: "Patients",
                column: "HospitalRegistrationNumber",
                principalTable: "Hospitals",
                principalColumn: "RegistrationNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
