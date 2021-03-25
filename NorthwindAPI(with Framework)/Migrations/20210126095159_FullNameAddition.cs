using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NorthwindAPI_with_Framework_.Migrations
{
    public partial class FullNameAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<String>(
                name: "FullName",
                table: "tbIUser",
                nullable:true
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
