using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelPlanner.Data.Migrations
{
    public partial class changedtripmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_AspNetUsers_ApplicationUserId",
                table: "Client");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Client",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Client_AspNetUsers_ApplicationUserId",
                table: "Client",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_AspNetUsers_ApplicationUserId",
                table: "Client");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Client",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Client_AspNetUsers_ApplicationUserId",
                table: "Client",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
