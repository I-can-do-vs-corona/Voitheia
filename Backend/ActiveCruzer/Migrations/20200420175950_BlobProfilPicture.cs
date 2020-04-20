using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ActiveCruzer.Migrations
{
    public partial class BlobProfilPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilPicture",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilPicture",
                table: "AspNetUsers");
        }
    }
}
