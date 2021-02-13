using Microsoft.EntityFrameworkCore.Migrations;

namespace CostPlanningServer.Migrations
{
    public partial class NtoNUserBase1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryUpdatedUser_Categories_CategoryId",
                table: "CategoryUpdatedUser");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderUpdatedUser_Orders_OrderId",
                table: "OrderUpdatedUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderUpdatedUser",
                table: "OrderUpdatedUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryUpdatedUser",
                table: "CategoryUpdatedUser");

            migrationBuilder.RenameTable(
                name: "OrderUpdatedUser",
                newName: "OrderUpdatedUsers");

            migrationBuilder.RenameTable(
                name: "CategoryUpdatedUser",
                newName: "CategoryUpdatedUsers");

            migrationBuilder.RenameIndex(
                name: "IX_OrderUpdatedUser_OrderId",
                table: "OrderUpdatedUsers",
                newName: "IX_OrderUpdatedUsers_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryUpdatedUser_CategoryId",
                table: "CategoryUpdatedUsers",
                newName: "IX_CategoryUpdatedUsers_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderUpdatedUsers",
                table: "OrderUpdatedUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryUpdatedUsers",
                table: "CategoryUpdatedUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryUpdatedUsers_Categories_CategoryId",
                table: "CategoryUpdatedUsers",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderUpdatedUsers_Orders_OrderId",
                table: "OrderUpdatedUsers",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryUpdatedUsers_Categories_CategoryId",
                table: "CategoryUpdatedUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderUpdatedUsers_Orders_OrderId",
                table: "OrderUpdatedUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderUpdatedUsers",
                table: "OrderUpdatedUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryUpdatedUsers",
                table: "CategoryUpdatedUsers");

            migrationBuilder.RenameTable(
                name: "OrderUpdatedUsers",
                newName: "OrderUpdatedUser");

            migrationBuilder.RenameTable(
                name: "CategoryUpdatedUsers",
                newName: "CategoryUpdatedUser");

            migrationBuilder.RenameIndex(
                name: "IX_OrderUpdatedUsers_OrderId",
                table: "OrderUpdatedUser",
                newName: "IX_OrderUpdatedUser_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryUpdatedUsers_CategoryId",
                table: "CategoryUpdatedUser",
                newName: "IX_CategoryUpdatedUser_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderUpdatedUser",
                table: "OrderUpdatedUser",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryUpdatedUser",
                table: "CategoryUpdatedUser",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryUpdatedUser_Categories_CategoryId",
                table: "CategoryUpdatedUser",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderUpdatedUser_Orders_OrderId",
                table: "OrderUpdatedUser",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
