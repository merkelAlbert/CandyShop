using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CandyShop.Migrations
{
    public partial class RelationShips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pastries_Orders_OrderId",
                table: "Pastries");

            migrationBuilder.DropIndex(
                name: "IX_Pastries_OrderId",
                table: "Pastries");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Pastries");

            migrationBuilder.CreateTable(
                name: "OrderPastry",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PastryId = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPastry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderPastry_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderPastry_Pastries_PastryId",
                        column: x => x.PastryId,
                        principalTable: "Pastries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderPastry_OrderId",
                table: "OrderPastry",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPastry_PastryId",
                table: "OrderPastry",
                column: "PastryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderPastry");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Pastries",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pastries_OrderId",
                table: "Pastries",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pastries_Orders_OrderId",
                table: "Pastries",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
