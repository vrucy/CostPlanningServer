using Microsoft.EntityFrameworkCore.Migrations;

namespace CostPlanningServer.Migrations
{
    public partial class SyncUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_SyncVisibleCategores_SyncVisible<Category>Id",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_SyncVisibleOrders_SyncVisible<Order>Id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_SyncVisible<Category>Id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_SyncVisible<Order>Id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_SyncVisibleOrders_ItemId",
                table: "SyncVisibleOrders");

            migrationBuilder.DropIndex(
                name: "IX_SyncVisibleCategores_ItemId",
                table: "SyncVisibleCategores");

            migrationBuilder.DropColumn(
                name: "SyncVisible<Category>Id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SyncVisible<Order>Id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "SyncUserCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    SyncId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyncUserCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SyncUserCategory_SyncVisibleCategores_SyncId",
                        column: x => x.SyncId,
                        principalTable: "SyncVisibleCategores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SyncUserOrder",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    SyncId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyncUserOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SyncUserOrder_SyncVisibleOrders_SyncId",
                        column: x => x.SyncId,
                        principalTable: "SyncVisibleOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SyncVisibleOrders_ItemId",
                table: "SyncVisibleOrders",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SyncVisibleCategores_ItemId",
                table: "SyncVisibleCategores",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SyncUserCategory_SyncId",
                table: "SyncUserCategory",
                column: "SyncId");

            migrationBuilder.CreateIndex(
                name: "IX_SyncUserOrder_SyncId",
                table: "SyncUserOrder",
                column: "SyncId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SyncUserCategory");

            migrationBuilder.DropTable(
                name: "SyncUserOrder");

            migrationBuilder.DropIndex(
                name: "IX_SyncVisibleOrders_ItemId",
                table: "SyncVisibleOrders");

            migrationBuilder.DropIndex(
                name: "IX_SyncVisibleCategores_ItemId",
                table: "SyncVisibleCategores");

            migrationBuilder.AddColumn<int>(
                name: "SyncVisible<Category>Id",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SyncVisible<Order>Id",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Users_SyncVisible<Category>Id",
                table: "Users",
                column: "SyncVisible<Category>Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SyncVisible<Order>Id",
                table: "Users",
                column: "SyncVisible<Order>Id");

            migrationBuilder.CreateIndex(
                name: "IX_SyncVisibleOrders_ItemId",
                table: "SyncVisibleOrders",
                column: "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SyncVisibleCategores_ItemId",
                table: "SyncVisibleCategores",
                column: "ItemId",
                unique: true);

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
    }
}
