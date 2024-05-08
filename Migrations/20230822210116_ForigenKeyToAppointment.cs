using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorAppointment.Migrations
{
    /// <inheritdoc />
    public partial class ForigenKeyToAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_DoctorsDoctorId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_PatientsPatientId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "PatientsPatientId",
                table: "Appointments",
                newName: "PatientId");

            migrationBuilder.RenameColumn(
                name: "DoctorsDoctorId",
                table: "Appointments",
                newName: "DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_PatientsPatientId",
                table: "Appointments",
                newName: "IX_Appointments_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_DoctorsDoctorId",
                table: "Appointments",
                newName: "IX_Appointments_DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors_DoctorId",
                table: "Appointments",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_DoctorId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Appointments",
                newName: "PatientsPatientId");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "Appointments",
                newName: "DoctorsDoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                newName: "IX_Appointments_PatientsPatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                newName: "IX_Appointments_DoctorsDoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors_DoctorsDoctorId",
                table: "Appointments",
                column: "DoctorsDoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PatientsPatientId",
                table: "Appointments",
                column: "PatientsPatientId",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
