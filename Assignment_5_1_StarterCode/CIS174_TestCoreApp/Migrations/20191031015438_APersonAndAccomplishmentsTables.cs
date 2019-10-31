﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CIS174_TestCoreApp.Migrations
{
    public partial class APersonAndAccomplishmentsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "TimeToCook",
                table: "Recipes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Birthdate = table.Column<DateTime>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "Accomplishments",
                columns: table => new
                {
                    AccomplishmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    DateOfAccomplishment = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accomplishments", x => x.AccomplishmentId);
                    table.ForeignKey(
                        name: "FK_Accomplishments_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accomplishments_PersonId",
                table: "Accomplishments",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accomplishments");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.AlterColumn<string>(
                name: "TimeToCook",
                table: "Recipes",
                nullable: true,
                oldClrType: typeof(TimeSpan));
        }
    }
}
