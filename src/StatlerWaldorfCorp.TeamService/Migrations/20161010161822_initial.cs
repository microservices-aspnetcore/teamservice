using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StatlerWaldorfCorp.TeamService.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsurePostgresExtension("uuid-ossp");
            //migrationBuilder.CreatePostgresExtension("uuid-ossp");

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false)
                        .Annotation("Npgsql:ValueGeneratedOnAdd", true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    TeamID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Members_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Members_TeamID",
                table: "Members",
                column: "TeamID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //   migrationBuilder.DropPostgresExtension("uuid-ossp");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
