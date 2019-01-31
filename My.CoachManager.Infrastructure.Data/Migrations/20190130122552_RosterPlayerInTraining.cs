using Microsoft.EntityFrameworkCore.Migrations;

namespace My.CoachManager.Infrastructure.Data.Migrations
{
    public partial class RosterPlayerInTraining : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingAttendances_Persons_PlayerId",
                table: "TrainingAttendances");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_TrainingAttendances_TrainingId_PlayerId",
                table: "TrainingAttendances");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "TrainingAttendances",
                newName: "RosterPlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_TrainingAttendances_PlayerId",
                table: "TrainingAttendances",
                newName: "IX_TrainingAttendances_RosterPlayerId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_TrainingAttendances_TrainingId_RosterPlayerId",
                table: "TrainingAttendances",
                columns: new[] { "TrainingId", "RosterPlayerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingAttendances_RosterPlayers_RosterPlayerId",
                table: "TrainingAttendances",
                column: "RosterPlayerId",
                principalTable: "RosterPlayers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingAttendances_RosterPlayers_RosterPlayerId",
                table: "TrainingAttendances");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_TrainingAttendances_TrainingId_RosterPlayerId",
                table: "TrainingAttendances");

            migrationBuilder.RenameColumn(
                name: "RosterPlayerId",
                table: "TrainingAttendances",
                newName: "PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_TrainingAttendances_RosterPlayerId",
                table: "TrainingAttendances",
                newName: "IX_TrainingAttendances_PlayerId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_TrainingAttendances_TrainingId_PlayerId",
                table: "TrainingAttendances",
                columns: new[] { "TrainingId", "PlayerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingAttendances_Persons_PlayerId",
                table: "TrainingAttendances",
                column: "PlayerId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
