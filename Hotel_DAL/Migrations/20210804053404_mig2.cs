using Microsoft.EntityFrameworkCore.Migrations;

namespace Hotel_DAL.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChangedTableName",
                table: "Logs",
                newName: "EntityName");

            migrationBuilder.AddColumn<int>(
                name: "EntityId",
                table: "Logs",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "Logs");

            migrationBuilder.RenameColumn(
                name: "EntityName",
                table: "Logs",
                newName: "ChangedTableName");
        }
    }
}
