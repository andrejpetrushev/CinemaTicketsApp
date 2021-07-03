using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopCinema.Web.Data.Migrations
{
    public partial class FirstTicketCinemaMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "firstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CinemaShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CartOwnerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CinemaShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CinemaShoppingCarts_AspNetUsers_CartOwnerId",
                        column: x => x.CartOwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MovieFullName = table.Column<string>(nullable: true),
                    MovieWatchType = table.Column<string>(nullable: true),
                    MovieHall = table.Column<int>(nullable: false),
                    RowNum = table.Column<int>(nullable: false),
                    SeatNum = table.Column<int>(nullable: false),
                    TicketMoviePrice = table.Column<int>(nullable: false),
                    MovieStartTime = table.Column<DateTime>(nullable: false),
                    MovieLengthTime = table.Column<DateTime>(nullable: false),
                    TicketUntilDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CinemaTicketInShoppingCarts",
                columns: table => new
                {
                    CinemaTicketId = table.Column<Guid>(nullable: false),
                    ShoppingCartId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CinemaTicketInShoppingCarts", x => new { x.CinemaTicketId, x.ShoppingCartId });
                    table.ForeignKey(
                        name: "FK_CinemaTicketInShoppingCarts_Tickets_CinemaTicketId",
                        column: x => x.CinemaTicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CinemaTicketInShoppingCarts_CinemaShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "CinemaShoppingCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CinemaShoppingCarts_CartOwnerId",
                table: "CinemaShoppingCarts",
                column: "CartOwnerId",
                unique: true,
                filter: "[CartOwnerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CinemaTicketInShoppingCarts_ShoppingCartId",
                table: "CinemaTicketInShoppingCarts",
                column: "ShoppingCartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CinemaTicketInShoppingCarts");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "CinemaShoppingCarts");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "firstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "lastName",
                table: "AspNetUsers");
        }
    }
}
