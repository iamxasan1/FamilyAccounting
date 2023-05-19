using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyAccounting.Data.Migrations
{
    public partial class asd123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_Families_FamilyId",
                table: "users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Incomes",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Expenses",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddForeignKey(
                name: "FK_users_Families_FamilyId",
                table: "users",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_Families_FamilyId",
                table: "users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Incomes",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Expenses",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddForeignKey(
                name: "FK_users_Families_FamilyId",
                table: "users",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id");
        }
    }
}
