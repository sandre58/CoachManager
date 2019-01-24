using Microsoft.EntityFrameworkCore.Migrations;

namespace My.CoachManager.Infrastructure.Data.Migrations
{
    public partial class InjuryPlayerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Injuries_Persons_PlayerId1",
                table: "Injuries");

            migrationBuilder.DropIndex(
                name: "IX_Injuries_PlayerId1",
                table: "Injuries");

            migrationBuilder.DropColumn(
                name: "PlayerId1",
                table: "Injuries");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerId1",
                table: "Injuries",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Injuries_PlayerId1",
                table: "Injuries",
                column: "PlayerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Injuries_Persons_PlayerId1",
                table: "Injuries",
                column: "PlayerId1",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
