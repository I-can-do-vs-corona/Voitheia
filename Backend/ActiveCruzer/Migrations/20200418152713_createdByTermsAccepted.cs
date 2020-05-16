using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ActiveCruzer.Migrations
{
    public partial class createdByTermsAccepted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Requests",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TermsAccepted",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "TermsAccepted",
                table: "AspNetUsers");
        }
    }
}
