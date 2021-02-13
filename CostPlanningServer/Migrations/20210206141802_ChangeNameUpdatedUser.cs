using Microsoft.EntityFrameworkCore.Migrations;

namespace CostPlanningServer.Migrations
{
    public partial class ChangeNameUpdatedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UpdetedUsers_Categories_CategoryId",
                table: "UpdetedUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_UpdetedUsers_Orders_OrderId",
                table: "UpdetedUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UpdetedUsers",
                table: "UpdetedUsers");

            migrationBuilder.RenameTable(
                name: "UpdetedUsers",
                newName: "UpdatedUsers");

            migrationBuilder.RenameIndex(
                name: "IX_UpdetedUsers_OrderId",
                table: "UpdatedUsers",
                newName: "IX_UpdatedUsers_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_UpdetedUsers_CategoryId",
                table: "UpdatedUsers",
                newName: "IX_UpdatedUsers_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UpdatedUsers",
                table: "UpdatedUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UpdatedUsers_Categories_CategoryId",
                table: "UpdatedUsers",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UpdatedUsers_Orders_OrderId",
                table: "UpdatedUsers",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UpdatedUsers_Categories_CategoryId",
                table: "UpdatedUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_UpdatedUsers_Orders_OrderId",
                table: "UpdatedUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UpdatedUsers",
                table: "UpdatedUsers");

            migrationBuilder.RenameTable(
                name: "UpdatedUsers",
                newName: "UpdetedUsers");

            migrationBuilder.RenameIndex(
                name: "IX_UpdatedUsers_OrderId",
                table: "UpdetedUsers",
                newName: "IX_UpdetedUsers_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_UpdatedUsers_CategoryId",
                table: "UpdetedUsers",
                newName: "IX_UpdetedUsers_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UpdetedUsers",
                table: "UpdetedUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UpdetedUsers_Categories_CategoryId",
                table: "UpdetedUsers",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UpdetedUsers_Orders_OrderId",
                table: "UpdetedUsers",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
