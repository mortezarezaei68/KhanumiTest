using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketManagement.Persistance.EF.Migrations
{
    public partial class Add_TrackingCode_in_Ticket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrackingCode",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrackingCode",
                table: "Tickets");
        }
    }
}
