using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LunchUp.Model.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", nullable: false),
                    Lastname = table.Column<string>(nullable: true),
                    Firstname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Photo = table.Column<string>(nullable: true),
                    Company = table.Column<string>(nullable: true),
                    OptIn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Response",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", nullable: false),
                    OriginId = table.Column<string>(nullable: true),
                    TargetId = table.Column<string>(nullable: true),
                    Like = table.Column<bool>(nullable: false),
                    ResponseDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Response", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Response_Person_OriginId",
                        column: x => x.OriginId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Response_Person_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Response_OriginId",
                table: "Response",
                column: "OriginId");

            migrationBuilder.CreateIndex(
                name: "IX_Response_TargetId",
                table: "Response",
                column: "TargetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Response");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
