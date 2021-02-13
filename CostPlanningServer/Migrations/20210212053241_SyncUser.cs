using Microsoft.EntityFrameworkCore.Migrations;

namespace CostPlanningServer.Migrations
{
    public partial class SyncUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SyncUserCategory_SyncVisibleCategores_SyncId",
                table: "SyncUserCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_SyncUserOrder_SyncVisibleOrders_SyncId",
                table: "SyncUserOrder");

            migrationBuilder.DropTable(
                name: "SyncVisibleCategores");

            migrationBuilder.DropTable(
                name: "SyncVisibleOrders");

            migrationBuilder.DropIndex(
                name: "IX_SyncUserOrder_SyncId",
                table: "SyncUserOrder");

            migrationBuilder.DropIndex(
                name: "IX_SyncUserCategory_SyncId",
                table: "SyncUserCategory");

            migrationBuilder.DropColumn(
                name: "SyncId",
                table: "SyncUserOrder");

            migrationBuilder.DropColumn(
                name: "SyncId",
                table: "SyncUserCategory");

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "SyncUserOrder",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "SyncUserCategory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Categories",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_SyncUserOrder_ItemId",
                table: "SyncUserOrder",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SyncUserCategory_ItemId",
                table: "SyncUserCategory",
                column: "ItemId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SyncUserCategory_Categories_ItemId",
                table: "SyncUserCategory",
                column: "ItemId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SyncUserOrder_Orders_ItemId",
                table: "SyncUserOrder",
                column: "ItemId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SyncUserCategory_Categories_ItemId",
                table: "SyncUserCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_SyncUserOrder_Orders_ItemId",
                table: "SyncUserOrder");

            migrationBuilder.DropIndex(
                name: "IX_SyncUserOrder_ItemId",
                table: "SyncUserOrder");

            migrationBuilder.DropIndex(
                name: "IX_SyncUserCategory_ItemId",
                table: "SyncUserCategory");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "SyncUserOrder");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "SyncUserCategory");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "SyncId",
                table: "SyncUserOrder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SyncId",
                table: "SyncUserCategory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SyncVisibleCategores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyncVisibleCategores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SyncVisibleCategores_Categories_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SyncVisibleOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyncVisibleOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SyncVisibleOrders_Orders_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SyncUserOrder_SyncId",
                table: "SyncUserOrder",
                column: "SyncId");

            migrationBuilder.CreateIndex(
                name: "IX_SyncUserCategory_SyncId",
                table: "SyncUserCategory",
                column: "SyncId");

            migrationBuilder.CreateIndex(
                name: "IX_SyncVisibleCategores_ItemId",
                table: "SyncVisibleCategores",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SyncVisibleOrders_ItemId",
                table: "SyncVisibleOrders",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_SyncUserCategory_SyncVisibleCategores_SyncId",
                table: "SyncUserCategory",
                column: "SyncId",
                principalTable: "SyncVisibleCategores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SyncUserOrder_SyncVisibleOrders_SyncId",
                table: "SyncUserOrder",
                column: "SyncId",
                principalTable: "SyncVisibleOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
