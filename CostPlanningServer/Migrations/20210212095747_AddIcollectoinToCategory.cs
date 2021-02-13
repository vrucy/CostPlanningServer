using Microsoft.EntityFrameworkCore.Migrations;

namespace CostPlanningServer.Migrations
{
    public partial class AddIcollectoinToCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SyncUserCategory_ItemId",
                table: "SyncUserCategory");

            migrationBuilder.CreateIndex(
                name: "IX_SyncUserCategory_ItemId",
                table: "SyncUserCategory",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SyncUserCategory_ItemId",
                table: "SyncUserCategory");

            migrationBuilder.CreateIndex(
                name: "IX_SyncUserCategory_ItemId",
                table: "SyncUserCategory",
                column: "ItemId",
                unique: true);
        }
    }
}
