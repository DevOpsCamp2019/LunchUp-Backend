using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LunchUp.Model.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Person",
                table => new
                {
                    Id = table.Column<string>("varchar(36)"),
                    Lastname = table.Column<string>(nullable: true),
                    Firstname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Photo = table.Column<string>(nullable: true),
                    Company = table.Column<string>(nullable: true),
                    OptIn = table.Column<DateTime>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Person", x => x.Id); });

            migrationBuilder.CreateTable(
                "Response",
                table => new
                {
                    Id = table.Column<string>("varchar(36)"),
                    OriginId = table.Column<string>(nullable: true),
                    TargetId = table.Column<string>(nullable: true),
                    Like = table.Column<bool>(),
                    ResponseDate = table.Column<DateTime>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Response", x => x.Id);
                    table.ForeignKey(
                        "FK_Response_Person_OriginId",
                        x => x.OriginId,
                        "Person",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Response_Person_TargetId",
                        x => x.TargetId,
                        "Person",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_Response_OriginId",
                "Response",
                "OriginId");

            migrationBuilder.CreateIndex(
                "IX_Response_TargetId",
                "Response",
                "TargetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Response");

            migrationBuilder.DropTable(
                "Person");
        }
    }
}