﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineLearning.EntityFramework.Migrations
{
    public partial class changecolumntoactive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Users",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
