using Microsoft.EntityFrameworkCore.Migrations;

namespace CostPlanningServer.Migrations
{
    public partial class fixDeviceModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Device_Users_UserId",
                table: "Device");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Device",
                table: "Device");

            migrationBuilder.RenameTable(
                name: "Device",
                newName: "Devices");

            migrationBuilder.RenameIndex(
                name: "IX_Device_UserId",
                table: "Devices",
                newName: "IX_Devices_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Devices",
                table: "Devices",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Users_UserId",
                table: "Devices",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Users_UserId",
                table: "Devices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Devices",
                table: "Devices");

            migrationBuilder.RenameTable(
                name: "Devices",
                newName: "Device");

            migrationBuilder.RenameIndex(
                name: "IX_Devices_UserId",
                table: "Device",
                newName: "IX_Device_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Device",
                table: "Device",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Device_Users_UserId",
                table: "Device",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
