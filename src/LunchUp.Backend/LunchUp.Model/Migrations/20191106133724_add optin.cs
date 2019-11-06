using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LunchUp.Model.Migrations
{
    public partial class addoptin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Person",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OptIn",
                table: "Person",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "OptIn",
                table: "Person");
        }
    }
}
