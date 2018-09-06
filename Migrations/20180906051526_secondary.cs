using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnBoarding.Migrations
{
    public partial class secondary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "UserSocialId",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "UserSocialId",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "UserSocialId",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "UserSocialId",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "EndUser",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "EndUser",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "EndUser",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "EndUser",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "Department",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Department",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "Department",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Department",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "Customer",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Customer",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "Customer",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Customer",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                table: "Agent",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Agent",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                table: "Agent",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Agent",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserSocialId");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "UserSocialId");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "UserSocialId");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "UserSocialId");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "EndUser");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "EndUser");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "EndUser");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "EndUser");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Agent");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Agent");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Agent");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Agent");
        }
    }
}
