using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace jce.DataAccess.Data.Migrations
{
    public partial class enabledPreson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          
           

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "JceProfiles",
                nullable: false,
                defaultValue: false);

         

      
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "JceProfiles");

           

         

        }
    }
}
