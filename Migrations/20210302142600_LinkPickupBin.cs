using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class LinkPickupBin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BinId",
                table: "Pickups",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pickups_BinId",
                table: "Pickups",
                column: "BinId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pickups_Bins_BinId",
                table: "Pickups",
                column: "BinId",
                principalTable: "Bins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pickups_Bins_BinId",
                table: "Pickups");

            migrationBuilder.DropIndex(
                name: "IX_Pickups_BinId",
                table: "Pickups");

            migrationBuilder.DropColumn(
                name: "BinId",
                table: "Pickups");
        }
    }
}
