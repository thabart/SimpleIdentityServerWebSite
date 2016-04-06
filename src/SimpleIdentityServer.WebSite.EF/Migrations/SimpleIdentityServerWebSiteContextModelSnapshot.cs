using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using SimpleIdentityServer.WebSite.EF;

namespace SimpleIdentityServer.WebSite.EF.Migrations
{
    [DbContext(typeof(SimpleIdentityServerWebSiteContext))]
    partial class SimpleIdentityServerWebSiteContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SimpleIdentityServer.WebSite.EF.Models.Profile", b =>
                {
                    b.Property<string>("Subject");

                    b.Property<string>("AuthorizationServerUrl");

                    b.Property<bool>("IsActive");

                    b.Property<string>("ManagerWebSiteApiUrl");

                    b.Property<string>("ManagerWebSiteUrl");

                    b.Property<string>("Name");

                    b.Property<string>("Picture");

                    b.HasKey("Subject");

                    b.HasAnnotation("Relational:TableName", "profiles");
                });
        }
    }
}
