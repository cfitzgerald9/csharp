using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelPlanner.Data.Migrations
{
    public partial class changedsomevariables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_AspNetUsers_ApplicationUserId1",
                table: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Client_ApplicationUserId1",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "Client");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Trip",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Client",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Trip_UserId",
                table: "Trip",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_ApplicationUserId",
                table: "Client",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_AspNetUsers_ApplicationUserId",
                table: "Client",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_AspNetUsers_UserId",
                table: "Trip",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_AspNetUsers_ApplicationUserId",
                table: "Client");

            migrationBuilder.DropForeignKey(
                name: "FK_Trip_AspNetUsers_UserId",
                table: "Trip");

            migrationBuilder.DropIndex(
                name: "IX_Trip_UserId",
                table: "Trip");

            migrationBuilder.DropIndex(
                name: "IX_Client_ApplicationUserId",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Trip");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "Client",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "Client",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Client_ApplicationUserId1",
                table: "Client",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_AspNetUsers_ApplicationUserId1",
                table: "Client",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
