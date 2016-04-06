using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace SimpleIdentityServer.WebSite.EF.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "profiles",
                columns: table => new
                {
                    Subject = table.Column<string>(nullable: false),
                    AuthorizationServerUrl = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    ManagerWebSiteApiUrl = table.Column<string>(nullable: true),
                    ManagerWebSiteUrl = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Picture = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.Subject);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("profiles");
        }
    }
}
