using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookTicket.Migrations
{
    /// <inheritdoc />
    public partial class changeticketentitymigr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovieTitle",
                table: "Screenings");

            migrationBuilder.RenameColumn(
                name: "BookingTime",
                table: "Tickets",
                newName: "ScreeningTime");

            migrationBuilder.AddColumn<int>(
                name: "Room",
                table: "Tickets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Tickets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ScreeningId",
                table: "Tickets",
                column: "ScreeningId");

            migrationBuilder.CreateIndex(
                name: "IX_Screenings_MovieId",
                table: "Screenings",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Screenings_Movies_MovieId",
                table: "Screenings",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Screenings_ScreeningId",
                table: "Tickets",
                column: "ScreeningId",
                principalTable: "Screenings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Screenings_Movies_MovieId",
                table: "Screenings");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Screenings_ScreeningId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_ScreeningId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Screenings_MovieId",
                table: "Screenings");

            migrationBuilder.DropColumn(
                name: "Room",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "ScreeningTime",
                table: "Tickets",
                newName: "BookingTime");

            migrationBuilder.AddColumn<string>(
                name: "MovieTitle",
                table: "Screenings",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
