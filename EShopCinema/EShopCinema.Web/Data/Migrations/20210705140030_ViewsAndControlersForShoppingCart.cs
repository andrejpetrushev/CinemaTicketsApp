using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopCinema.Web.Data.Migrations
{
    public partial class ViewsAndControlersForShoppingCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CinemaShoppingCartId1",
                table: "CinemaTicketInShoppingCarts",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserCartId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CinemaTicketsInOrders",
                columns: table => new
                {
                    CinemaTicketId = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CinemaTicketsInOrders", x => new { x.CinemaTicketId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_CinemaTicketsInOrders_Orders_CinemaTicketId",
                        column: x => x.CinemaTicketId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CinemaTicketsInOrders_Tickets_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CinemaTicketInShoppingCarts_CinemaShoppingCartId1",
                table: "CinemaTicketInShoppingCarts",
                column: "CinemaShoppingCartId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserCartId",
                table: "AspNetUsers",
                column: "UserCartId");

            migrationBuilder.CreateIndex(
                name: "IX_CinemaTicketsInOrders_OrderId",
                table: "CinemaTicketsInOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CinemaShoppingCarts_UserCartId",
                table: "AspNetUsers",
                column: "UserCartId",
                principalTable: "CinemaShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CinemaTicketInShoppingCarts_CinemaShoppingCarts_CinemaShoppingCartId1",
                table: "CinemaTicketInShoppingCarts",
                column: "CinemaShoppingCartId1",
                principalTable: "CinemaShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CinemaShoppingCarts_UserCartId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CinemaTicketInShoppingCarts_CinemaShoppingCarts_CinemaShoppingCartId1",
                table: "CinemaTicketInShoppingCarts");

            migrationBuilder.DropTable(
                name: "CinemaTicketsInOrders");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_CinemaTicketInShoppingCarts_CinemaShoppingCartId1",
                table: "CinemaTicketInShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserCartId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CinemaShoppingCartId1",
                table: "CinemaTicketInShoppingCarts");

            migrationBuilder.DropColumn(
                name: "UserCartId",
                table: "AspNetUsers");
        }
    }
}
