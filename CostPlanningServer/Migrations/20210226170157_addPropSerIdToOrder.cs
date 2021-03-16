using Microsoft.EntityFrameworkCore.Migrations;

namespace CostPlanningServer.Migrations
{
    public partial class addPropSerIdToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServerId",
                table: "Orders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServerId",
                table: "Orders");
        }
    }
}
