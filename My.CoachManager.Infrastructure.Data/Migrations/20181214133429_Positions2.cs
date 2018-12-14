using Microsoft.EntityFrameworkCore.Migrations;

namespace My.CoachManager.Infrastructure.Data.Migrations
{
    public partial class Positions2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerPositions_Positions_PlayerId",
                table: "PlayerPositions");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerPositions_Persons_PositionId",
                table: "PlayerPositions");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerPositions_Persons_PlayerId",
                table: "PlayerPositions",
                column: "PlayerId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerPositions_Positions_PositionId",
                table: "PlayerPositions",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerPositions_Persons_PlayerId",
                table: "PlayerPositions");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerPositions_Positions_PositionId",
                table: "PlayerPositions");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerPositions_Positions_PlayerId",
                table: "PlayerPositions",
                column: "PlayerId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerPositions_Persons_PositionId",
                table: "PlayerPositions",
                column: "PositionId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
