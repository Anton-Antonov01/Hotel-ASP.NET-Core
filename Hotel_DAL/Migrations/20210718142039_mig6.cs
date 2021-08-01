using Microsoft.EntityFrameworkCore.Migrations;

namespace Hotel_DAL.Migrations
{
    public partial class mig6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AspNetUsers_GuestId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "GuestId",
                table: "Bookings",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_GuestId",
                table: "Bookings",
                newName: "IX_Bookings_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AspNetUsers_UserId",
                table: "Bookings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AspNetUsers_UserId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Bookings",
                newName: "GuestId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                newName: "IX_Bookings_GuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AspNetUsers_GuestId",
                table: "Bookings",
                column: "GuestId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
