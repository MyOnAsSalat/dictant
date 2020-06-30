using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Dictant.Server.Migrations.TasksDb
{
    public partial class Dictant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dictants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    Timings = table.Column<string>(nullable: true),
                    AudioSource = table.Column<string>(nullable: true),
                    OwnerId = table.Column<string>(nullable: true),
                    ApproverId = table.Column<string>(nullable: true),
                    Approved = table.Column<bool>(nullable: false),
                    Public = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dictants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attempts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DictantSource = table.Column<int>(nullable: false),
                    User = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Start = table.Column<DateTimeOffset>(nullable: false),
                    End = table.Column<DateTimeOffset>(nullable: false),
                    Finished = table.Column<bool>(nullable: false),
                    Result = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attempts_Dictants_DictantSource",
                        column: x => x.DictantSource,
                        principalTable: "Dictants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(nullable: true),
                    DictantId = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    OwnerId = table.Column<string>(nullable: false),
                    Start = table.Column<DateTimeOffset>(nullable: false),
                    End = table.Column<DateTimeOffset>(nullable: false),
                    Participants = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exams_Dictants_DictantId",
                        column: x => x.DictantId,
                        principalTable: "Dictants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attempts_DictantSource",
                table: "Attempts",
                column: "DictantSource");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_DictantId",
                table: "Exams",
                column: "DictantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attempts");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "Dictants");
        }
    }
}
