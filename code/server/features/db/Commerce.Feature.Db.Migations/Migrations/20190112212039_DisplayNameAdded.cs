using Microsoft.EntityFrameworkCore.Migrations;

namespace Commerce.Feature.Db.Common.Migrations
{
    public partial class DisplayNameAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "Products");
        }
    }
}
