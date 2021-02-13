using Microsoft.EntityFrameworkCore.Migrations;

namespace CostPlanningServer.Migrations
{
    public partial class SyncVisible1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SyncVisible<Category>_Categories_ItemId",
                table: "SyncVisible<Category>");

            migrationBuilder.DropForeignKey(
                name: "FK_SyncVisible<Order>_Orders_ItemId",
                table: "SyncVisible<Order>");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_SyncVisible<Category>_SyncVisible<Category>Id",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_SyncVisible<Order>_SyncVisible<Order>Id",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SyncVisible<Order>",
                table: "SyncVisible<Order>");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SyncVisible<Category>",
                table: "SyncVisible<Category>");

            migrationBuilder.RenameTable(
                name: "SyncVisible<Order>",
                newName: "SyncVisibleOrders");

            migrationBuilder.RenameTable(
                name: "SyncVisible<Category>",
                newName: "SyncVisibleCategores");

            migrationBuilder.RenameIndex(
                name: "IX_SyncVisible<Order>_ItemId",
                table: "SyncVisibleOrders",
                newName: "IX_SyncVisibleOrders_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_SyncVisible<Category>_ItemId",
                table: "SyncVisibleCategores",
                newName: "IX_SyncVisibleCategores_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SyncVisibleOrders",
                table: "SyncVisibleOrders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SyncVisibleCategores",
                table: "SyncVisibleCategores",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SyncVisibleCategores_Categories_ItemId",
                table: "SyncVisibleCategores",
                column: "ItemId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SyncVisibleOrders_Orders_ItemId",
                table: "SyncVisibleOrders",
                column: "ItemId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_SyncVisibleCategores_SyncVisible<Category>Id",
                table: "Users",
                column: "SyncVisible<Category>Id",
                principalTable: "SyncVisibleCategores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_SyncVisibleOrders_SyncVisible<Order>Id",
                table: "Users",
                column: "SyncVisible<Order>Id",
                principalTable: "SyncVisibleOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SyncVisibleCategores_Categories_ItemId",
                table: "SyncVisibleCategores");

            migrationBuilder.DropForeignKey(
                name: "FK_SyncVisibleOrders_Orders_ItemId",
                table: "SyncVisibleOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_SyncVisibleCategores_SyncVisible<Category>Id",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_SyncVisibleOrders_SyncVisible<Order>Id",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SyncVisibleOrders",
                table: "SyncVisibleOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SyncVisibleCategores",
                table: "SyncVisibleCategores");

            migrationBuilder.RenameTable(
                name: "SyncVisibleOrders",
                newName: "SyncVisible<Order>");

            migrationBuilder.RenameTable(
                name: "SyncVisibleCategores",
                newName: "SyncVisible<Category>");

            migrationBuilder.RenameIndex(
                name: "IX_SyncVisibleOrders_ItemId",
                table: "SyncVisible<Order>",
                newName: "IX_SyncVisible<Order>_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_SyncVisibleCategores_ItemId",
                table: "SyncVisible<Category>",
                newName: "IX_SyncVisible<Category>_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SyncVisible<Order>",
                table: "SyncVisible<Order>",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SyncVisible<Category>",
                table: "SyncVisible<Category>",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SyncVisible<Category>_Categories_ItemId",
                table: "SyncVisible<Category>",
                column: "ItemId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SyncVisible<Order>_Orders_ItemId",
                table: "SyncVisible<Order>",
                column: "ItemId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_SyncVisible<Category>_SyncVisible<Category>Id",
                table: "Users",
                column: "SyncVisible<Category>Id",
                principalTable: "SyncVisible<Category>",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_SyncVisible<Order>_SyncVisible<Order>Id",
                table: "Users",
                column: "SyncVisible<Order>Id",
                principalTable: "SyncVisible<Order>",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
