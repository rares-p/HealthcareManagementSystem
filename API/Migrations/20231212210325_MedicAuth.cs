﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class MedicAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                schema: "healthcaremanagementsystem",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Password",
                schema: "healthcaremanagementsystem",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "healthcaremanagementsystem",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Username",
                schema: "healthcaremanagementsystem",
                table: "Users",
                newName: "AuthDataId");

            migrationBuilder.RenameColumn(
                name: "Email",
                schema: "healthcaremanagementsystem",
                table: "Medics",
                newName: "AuthDataId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthDataId",
                schema: "healthcaremanagementsystem",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "AuthDataId",
                schema: "healthcaremanagementsystem",
                table: "Medics",
                newName: "Email");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "healthcaremanagementsystem",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                schema: "healthcaremanagementsystem",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "healthcaremanagementsystem",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
