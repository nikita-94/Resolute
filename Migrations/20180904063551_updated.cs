using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnBoarding.Migrations
{
    public partial class updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EndUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone_no = table.Column<string>(nullable: true),
                    Profile_img_url = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EndUser_Customer_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserSocialId",
                columns: table => new
                {
                    SocialId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Source = table.Column<string>(nullable: true),
                    Identifier = table.Column<string>(nullable: true),
                    EndUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSocialId", x => x.SocialId);
                    table.ForeignKey(
                        name: "FK_UserSocialId_EndUser_EndUserId",
                        column: x => x.EndUserId,
                        principalTable: "EndUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EndUser_OrganizationId",
                table: "EndUser",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSocialId_EndUserId",
                table: "UserSocialId",
                column: "EndUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSocialId");

            migrationBuilder.DropTable(
                name: "EndUser");
        }
    }
}
