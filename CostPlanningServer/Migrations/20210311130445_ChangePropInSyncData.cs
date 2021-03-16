using Microsoft.EntityFrameworkCore.Migrations;

namespace CostPlanningServer.Migrations
{
    public partial class ChangePropInSyncData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SyncUserCategory");

            migrationBuilder.DropTable(
                name: "SyncUserOrder");

            migrationBuilder.CreateTable(
                name: "SyncDataCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<string>(nullable: true),
                    ItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyncDataCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SyncDataCategory_Categories_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SyncDataOrder",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<string>(nullable: true),
                    ItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyncDataOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SyncDataOrder_Orders_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SyncDataCategory_ItemId",
                table: "SyncDataCategory",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SyncDataOrder_ItemId",
                table: "SyncDataOrder",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SyncDataCategory");

            migrationBuilder.DropTable(
                name: "SyncDataOrder");

            migrationBuilder.CreateTable(
                name: "SyncUserCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyncUserCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SyncUserCategory_Categories_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SyncUserOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyncUserOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SyncUserOrder_Orders_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SyncUserCategory_ItemId",
                table: "SyncUserCategory",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SyncUserOrder_ItemId",
                table: "SyncUserOrder",
                column: "ItemId");
        }
    }
}
