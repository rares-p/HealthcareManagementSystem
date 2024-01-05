using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class asd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicationReminders_Medications_MedicationId",
                schema: "healthcaremanagementsystem",
                table: "MedicationReminders");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationReminders_Users_UserId",
                schema: "healthcaremanagementsystem",
                table: "MedicationReminders");

            migrationBuilder.DropIndex(
                name: "IX_MedicationReminders_MedicationId",
                schema: "healthcaremanagementsystem",
                table: "MedicationReminders");

            migrationBuilder.DropIndex(
                name: "IX_MedicationReminders_UserId",
                schema: "healthcaremanagementsystem",
                table: "MedicationReminders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MedicationReminders_MedicationId",
                schema: "healthcaremanagementsystem",
                table: "MedicationReminders",
                column: "MedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationReminders_UserId",
                schema: "healthcaremanagementsystem",
                table: "MedicationReminders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationReminders_Medications_MedicationId",
                schema: "healthcaremanagementsystem",
                table: "MedicationReminders",
                column: "MedicationId",
                principalSchema: "healthcaremanagementsystem",
                principalTable: "Medications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationReminders_Users_UserId",
                schema: "healthcaremanagementsystem",
                table: "MedicationReminders",
                column: "UserId",
                principalSchema: "healthcaremanagementsystem",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
