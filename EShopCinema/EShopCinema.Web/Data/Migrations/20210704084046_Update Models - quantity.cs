using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopCinema.Web.Data.Migrations
{
    public partial class UpdateModelsquantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicketsQuantity",
                table: "CinemaTicketInShoppingCarts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketsQuantity",
                table: "CinemaTicketInShoppingCarts");
        }
    }
}
