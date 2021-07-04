using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopCinema.Web.Data.Migrations
{
    public partial class ChangeInTicketCinemaModelEndTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MovieEndTime",
                table: "Tickets",
                newName: "MovieENDTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MovieENDTime",
                table: "Tickets",
                newName: "MovieEndTime");
        }
    }
}
