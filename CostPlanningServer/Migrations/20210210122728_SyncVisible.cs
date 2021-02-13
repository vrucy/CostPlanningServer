using Microsoft.EntityFrameworkCore.Migrations;

namespace CostPlanningServer.Migrations
{
    public partial class SyncVisible : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryUpdatedUsers");

            migrationBuilder.DropTable(
                name: "OrderUpdatedUsers");

            migrationBuilder.AddColumn<int>(
                name: "SyncVisible<Category>Id",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SyncVisible<Order>Id",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SyncVisible<Category>",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsVisible = table.Column<bool>(nullable: false),
                    ItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyncVisible<Category>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SyncVisible<Category>_Categories_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SyncVisible<Order>",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsVisible = table.Column<bool>(nullable: false),
                    ItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyncVisible<Order>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SyncVisible<Order>_Orders_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_SyncVisible<Category>Id",
                table: "Users",
                column: "SyncVisible<Category>Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SyncVisible<Order>Id",
                table: "Users",
                column: "SyncVisible<Order>Id");

            migrationBuilder.CreateIndex(
                name: "IX_SyncVisible<Category>_ItemId",
                table: "SyncVisible<Category>",
                column: "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SyncVisible<Order>_ItemId",
                table: "SyncVisible<Order>",
                column: "ItemId",
                unique: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_SyncVisible<Category>_SyncVisible<Category>Id",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_SyncVisible<Order>_SyncVisible<Order>Id",
                table: "Users");

            migrationBuilder.DropTable(
                name: "SyncVisible<Category>");

            migrationBuilder.DropTable(
                name: "SyncVisible<Order>");

            migrationBuilder.DropIndex(
                name: "IX_Users_SyncVisible<Category>Id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_SyncVisible<Order>Id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SyncVisible<Category>Id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SyncVisible<Order>Id",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "CategoryUpdatedUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryUpdatedUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryUpdatedUsers_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderUpdatedUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderUpdatedUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderUpdatedUsers_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryUpdatedUsers_CategoryId",
                table: "CategoryUpdatedUsers",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderUpdatedUsers_OrderId",
                table: "OrderUpdatedUsers",
                column: "OrderId");
        }
    }
}
