using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hotel_DAL.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Guests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Guests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Guests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Guests",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Guests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Guests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Guests");
        }
    }
}
