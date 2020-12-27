using Microsoft.EntityFrameworkCore.Migrations;

namespace CostPlanningServer.Migrations
{
    public partial class addServeridinOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsWriteToDb",
                table: "Orders");

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

            migrationBuilder.AddColumn<bool>(
                name: "IsWriteToDb",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
